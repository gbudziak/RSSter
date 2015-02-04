using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Models.RSS;
using Models.ViewModels;
using Newtonsoft.Json;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class  SearchResultsBuilder:ISearchResultsBuilder
    {
        private readonly IApplicationRssDataContext _rssDatabase;

        public SearchResultsBuilder(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public IEnumerable<Channel> GetAllChannels()
        {
            var model = _rssDatabase.Channels.Select(x => x);
            return model;
        }

        public IEnumerable<Item> GetAllItems()
        {
            var model = _rssDatabase.AllItems.Select(x => x);
            return model;
        }

        public List<SearchChannel> SearchForString(string searchString)
        {

            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    var resultModels = new List<SearchChannel>();

                    searchString = RemoveDiacritics(searchString);
                    string[] wordsToMatch = SplitText(searchString);

                    var model = GetAllChannels();

                    var containsFullTextInUrlOrTitleOrDescription = model.Where(x => x.Url.ToLower().Contains(searchString) ||
                                                                    x.Title.ToLower().Contains(searchString) ||
                                                                    x.Description.ToLower().Contains(searchString))
                                                                    .OrderBy(x => x.Readers).ToList();

                    foreach (var channel in containsFullTextInUrlOrTitleOrDescription)
                    {
                        var foundChannel = Mapper.Map<Channel, SearchChannel>(channel);
                        foundChannel.Rating = 1000;
                        resultModels.Add(foundChannel);
                    }

                    var containsAllWordsInUrlTitleOrDescription = from channel in model
                                                                  let url = SplitText(RemoveDiacritics(channel.Url))
                                                                  let description = SplitText(RemoveDiacritics(channel.Description))
                                                                  let title = SplitText(RemoveDiacritics(channel.Title))
                                                                  where (url.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                                                      || description.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                                                      || title.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count())
                                                                  select channel;

                    var toMerge = new List<SearchChannel>();
                    foreach (var channel in containsAllWordsInUrlTitleOrDescription)
                    {
                        var foundChannel = Mapper.Map<Channel, SearchChannel>(channel);
                        foundChannel.Rating = 100;

                        toMerge.Add(foundChannel);
                    }

                    if (wordsToMatch.Count() >= 2)
                    {
                        var containsHalfWordsInUrlOrTitleOrDescription = from channel in model
                                                                         let url = SplitText(channel.Url)
                                                                         let description = SplitText(channel.Description)
                                                                         let title = SplitText(channel.Title)
                                                                         where (url.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / 2)
                                                                             || description.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / 2)
                                                                             || title.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / 2))
                                                                         select channel;

                        var ttt = containsHalfWordsInUrlOrTitleOrDescription.OrderBy(x => x.Readers).ToList();

                        foreach (var channel in containsHalfWordsInUrlOrTitleOrDescription)
                        {
                            var foundChannel = Mapper.Map<Channel, SearchChannel>(channel);
                            foundChannel.Rating = 10;

                            toMerge.Add(foundChannel);
                        }

                    }

                    var foo = ListComparer(resultModels, toMerge);
                    var result = foo.OrderByDescending(x => x.Rating).ThenByDescending(x => x.Readers).ToList();
                    //string jsonResult = JsonConvert.SerializeObject(foo);
                    //string json = JsonConvert.SerializeObject(foo , Formatting.Indented);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }
        private string[] SplitText(string wordsToSplit)
        {
            return wordsToSplit.ToLower().Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },StringSplitOptions.RemoveEmptyEntries);
        }

        private string RemoveDiacritics(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                .Normalize(NormalizationForm.FormC);
        }


        private List<SearchChannel> ListComparer(List<SearchChannel> list1, List<SearchChannel> list2)
        {
            var dict = list1.ToDictionary(x => x.Id);
            foreach (var channel in list2)
            {
               try
               {
                 channel.Rating += dict[channel.Id].Rating;
               }
               catch
               {
               }

                dict[channel.Id] = channel;
            }
            var merged = dict.Values.ToList();
            return merged;
        }

    }
                  
}

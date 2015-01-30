using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Models.RSS;
using Models.ViewModels;
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

        public IEnumerable<Channel> SearchForString(string searchString)
        {
            var resultModels = new List<SearchChannel>();

            searchString = RemoveDiacritics(searchString);
            string[] wordsToMatch = SplitText(searchString);

            var model = GetAllChannels();

            var containsFullTextInUrlOrTitleOrDescription = model.Where(x => x.Url.ToLower().Contains(searchString) ||
                                                            x.Title.ToLower().Contains(searchString) ||
                                                            x.Description.ToLower().Contains(searchString))
                                                            .OrderBy(x => x.Readers);

            var zzz = containsFullTextInUrlOrTitleOrDescription.OrderBy(x=>x.Readers).ToList();

            var containsAllWordsInUrlTitleOrDescription = from channel in model
                                                            let url = SplitText(RemoveDiacritics(channel.Url))
                                                            let description = SplitText(RemoveDiacritics(channel.Description))
                                                            let title = SplitText(RemoveDiacritics(channel.Title))
                                                            where (url.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count() 
                                                                || description.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                                                || title.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count())
                                                            select channel;

            var xxx = containsAllWordsInUrlTitleOrDescription.OrderBy(x => x.Readers).ToList();
            var merged = zzz.Union(xxx, new ChannelComparer());

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
                merged = merged.Union(ttt, new ChannelComparer());

            }

            
            return merged;

        }
        public string[] SplitText(string wordsToSplit)
        {
            return wordsToSplit.ToLower().Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },StringSplitOptions.RemoveEmptyEntries);
        }

        static string RemoveDiacritics(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                              UnicodeCategory.NonSpacingMark)
              ).Normalize(NormalizationForm.FormC);
        }

        class ChannelComparer : IEqualityComparer<Channel>
        {
            public bool Equals(Channel x, Channel y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Channel obj)
            {
                return 1;
            }
        }
    }
                  
}

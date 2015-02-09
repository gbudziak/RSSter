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
using Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Services.RssReader.Implementation
{
    public class  SearchResultsBuilder:ISearchResultsBuilder
    {
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IdentityDbContext _userDatabase = new IdentityDbContext();

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

        public List<IdentityUser> GetAllUsers()
        {
            var model = _userDatabase.Users.Select(x => x).ToList();
            return model;
        }

        public Search MainSearch(string searchString)
        {
            var UserList = SearchUsersForString(searchString);
            var ChannelList = SearchChannelsForString(searchString);

            var model = new Search();
            model.ChannelList = ChannelList;
            model.UsersList = UserList;
            return model;
        }

        public List<IdentityUser> SearchUsersForString(string searchString)
        {
            var UserList = GetAllUsers();
            var containsFullTextInEmailOrUserName = UserList.Where(
                                                            x => x.Email.ToLower().Contains(searchString) ||
                                                            x.UserName.ToLower().Contains(searchString))
                                                            .OrderBy(x => x.Email).ToList();
            return containsFullTextInEmailOrUserName;
        }

        public List<SearchChannel> SearchChannelsForString(string searchString)
        {
            var allChannels = GetAllChannels();
            var ChannelList = new List<SearchChannel>();
            var toMerge = new List<SearchChannel>();
            searchString = RemoveDiacritics(searchString);
            string[] wordsToMatch = ToLowerAndSplitText(searchString);
            
            var containsFullTextInUrlOrTitleOrDescription = allChannels.Where(
                                                            x => x.Url.ToLower().Contains(searchString) ||
                                                            x.Title.ToLower().Contains(searchString) ||
                                                            x.Description.ToLower().Contains(searchString))
                                                            .OrderBy(x => x.Readers).ToList();

            MapToSearchChannelAddRatingAndAddToList(containsFullTextInUrlOrTitleOrDescription, toMerge, 1000);

            var containsAllWordsInUrlTitleOrDescription = SearchForStringsInChannelList(allChannels, wordsToMatch, 1);
            MapToSearchChannelAddRatingAndAddToList(containsAllWordsInUrlTitleOrDescription, toMerge,100);

            if (wordsToMatch.Count() >= 2)
            {
                var containsHalfWordsInUrlOrTitleOrDescription = SearchForStringsInChannelList(allChannels, wordsToMatch, 2);
                MapToSearchChannelAddRatingAndAddToList(containsHalfWordsInUrlOrTitleOrDescription, toMerge, 10);
            }

            var ChannelListCompare = ListComparer(ChannelList, toMerge);
            var result = ChannelListCompare.OrderByDescending(x => x.Rating).ThenByDescending(x => x.Readers).ToList();

            return result;
        }

        private IEnumerable<Channel> SearchForStringsInChannelList(IEnumerable<Channel> ChannelList, string[] wordsToMatch, int divisor)
        {
            var returnModel = from channel in ChannelList
                              let url = ToLowerAndSplitText(RemoveDiacritics(channel.Url))
                              let description = ToLowerAndSplitText(RemoveDiacritics(channel.Description))
                              let title = ToLowerAndSplitText(RemoveDiacritics(channel.Title))
                              where (url.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / divisor)
                                  || description.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / divisor)
                                  || title.Distinct().Intersect(wordsToMatch).Count() == (wordsToMatch.Count() / divisor))
                              select channel;

            return returnModel;
        }

        private void MapToSearchChannelAddRatingAndAddToList(IEnumerable<Channel> channelList, List<SearchChannel> searchChannelList, int rating)
        {
            foreach (var channel in channelList)
            {
                var mappedChannel = Mapper.Map<Channel, SearchChannel>(channel);
                mappedChannel.Rating = rating;
                searchChannelList.Add(mappedChannel);
            }
        }

        private string[] SplitText(string wordsToSplit)
        {
            return wordsToSplit.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] ToLowerAndSplitText(string wordsToSplit)
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
                   // ignored
               }

                dict[channel.Id] = channel;
            }

            var merged = dict.Values.ToList();
            return merged;
        }

    }
                  
}

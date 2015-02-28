using Models.RSS;
using RssDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Models.ViewModels;

namespace Services.RssReader.Implementation
{
    public class UserHistoryService : IUserHistoryService
    {

        private readonly IApplicationRssDataContext _rssDatabase;


        public UserHistoryService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public void AddToHistory(HistoryAction actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId)
        {
            var userHistory = new UserHistory() { HistoryActionName = actionName, ApplicationUserId = userId, Date = date, SubscriptionId = subscriptionId, UserChannelId = userChannelId, UserItemId = userItemId };
            _rssDatabase.UsersHistory.Add(userHistory);
            _rssDatabase.SaveChanges();

        }

        public List<UserHistoryViewModel> ShowUserHistory(string userId, string userName)
        {
            var model = _rssDatabase.UsersHistory.Where(history => history.ApplicationUserId == userId)
                .ToList();

            var userChannels = _rssDatabase.UserChannels
                .Include(x => x.Channel)
                .Include(x => x.UserItems)
                .Where(x => x.ApplicationUserId == userId).ToList();

            var userItems = _rssDatabase.UsersItems
                .Include(x => x.Item)
                .Where(x => x.ApplicationUserId == userId).ToList();

            var userHistoryViewModel = new List<UserHistoryViewModel>();

            foreach (var historyItem in model)
            {
                var history = Mapper.Map<UserHistory, UserHistoryViewModel>(historyItem);
                history.UserName = userName;
                if (history.HistoryActionName == HistoryAction.AddChannel || 
                    history.HistoryActionName == HistoryAction.RemoveChannel)
                {
                    history.Channel = returnChannel(userChannels, history);

                }

                if (history.HistoryActionName == HistoryAction.RatingMinus ||
                    history.HistoryActionName == HistoryAction.RatingPlus ||
                    history.HistoryActionName == HistoryAction.Read)
                {
                    history.Item = returnItem(userItems, history);
                }

                //if (history.HistoryActionName == HistoryAction.AddSubscription || 
                //    history.HistoryActionName == HistoryAction.RemoveSubscription)
                //{
                    
                //}
                userHistoryViewModel.Add(history);

            }
            return userHistoryViewModel.OrderByDescending(x => x.Date).ToList();
        }

        private Channel returnChannel(List<UserChannel> userChannels, UserHistoryViewModel history)
        {
            return userChannels
                    .Select(x => x.Channel)
                    .Single(x => x.Id == history.UserChannelId);
        }

        private Item returnItem(List<UserItem> userItems, UserHistoryViewModel history)
        {

            var itemId = userItems.First(x => x.Id == history.UserItemId).ItemId;
            return userItems
                    .Select(x => x.Item)
                    .Single(x => x.Id == itemId);
        }






        public List<UserHistoryViewModel> ShowUserHistory(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

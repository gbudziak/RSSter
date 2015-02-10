using Microsoft.AspNet.Identity.EntityFramework;
using Models.RSS;
using RssDataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Models.ViewModels;

namespace Services.RssReader.Implementation
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IdentityDbContext _userDatabase = new IdentityDbContext();

        public SubscriptionService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public List<UserSubscriptions> GetUserSubscriptions(string userId)
        {
            var model = _rssDatabase.AllUserSubscriptions.Where(x => x.ApplicationUserId == userId).ToList();
            return model;
        }

        public void AddSubscription(string userId, string subscriptionId, string subscriptionEmail)
        {
            var newSubscription = new UserSubscriptions(userId,subscriptionId, subscriptionEmail);
            _rssDatabase.AllUserSubscriptions.Add(newSubscription);
            _rssDatabase.SaveChanges();


        }

        public SubscriptionViewModel GetSubscriptionModel(string subscriptionId)
        {
            var subscribtion = _userDatabase.Users.Single(x => x.Id == subscriptionId);
            //var model = new SubscriptionViewModel();
            //model.Email = subscribtion.Email;
            //model.Id = subscribtion.Id;
            //model.UserName = subscribtion.UserName;
            
            var mappedSubscription = Mapper.Map<IdentityUser, SubscriptionViewModel>(subscribtion);
            var channels = _rssDatabase.UserChannels.Where(x => x.ApplicationUserId == subscriptionId).ToList();
            
            mappedSubscription.Channels = channels;

            return mappedSubscription;
        }
    }
}

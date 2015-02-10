using System;
using System.Data.Entity;
using System.Web;
using RssDataContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Models.Models;
using RSSter.Controllers;
using Services.RssReader;
using Services.RssReader.Implementation;


namespace RSSter
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public UnityConfig()
        {
        }

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IChannelGet, ChannelGet>();
            container.RegisterType<IChannelService, ChannelService>();
            container.RegisterType<IApplicationRssDataContext, ApplicationRssDataContext>();
            container.RegisterType<IValidateService, ValidateService>();
            container.RegisterType<IChannelProvider, ChannelProvider>();
            container.RegisterType<ISyndicationFeedProvider, SyndicationFeedProvider>();
            container.RegisterType<IViewService, ViewService>();
            container.RegisterType<ISearchResultsBuilder, SearchResultsBuilder>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ISubscriptionService, SubscriptionService>();
            container.RegisterType<IUserHistoryService, UserHistoryService>();



            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, ApplicationRssDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

        }
    }
}

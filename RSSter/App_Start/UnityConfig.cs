using System;
using Microsoft.Practices.Unity;
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

            container.RegisterType<IDownloadChannelItemsList, downloadChannelItemsList>();
            container.RegisterType<IChannelService, ChannelService>();
            container.RegisterType<IDatabase, FakeDb>();
        }
    }
}

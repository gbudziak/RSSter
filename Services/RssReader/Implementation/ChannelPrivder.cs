using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelPrivder : ISyndicateFeed
    {
        private readonly ISyndicateFeedProvider _syndicateFeedProvider;

        public ChannelPrivder(ISyndicateFeedProvider syndicateFeedProvider)
        {
            _syndicateFeedProvider = syndicateFeedProvider;
        }

        public Channel GetChannel(string url)
        {
            var feed = _syndicateFeedProvider.GetSyndicationFeed(url);
            var channelModel = ParseFeedToChannelWithItem(url, feed);
            return channelModel;
        }

        private static Channel ParseFeedToChannelWithItem(string url, SyndicationFeed feed)
        {
            var itemList = new List<Item>();

            if (feed != null)
            {
                foreach (SyndicationItem item in feed.Items)
                {
                    var item = Mapper.Map<SyndicationItem, Item>(item);
                    itemList.Add(item);

                    //itemList.Add(new Item
                    //{
                    //    Description = item.Summary.Text,
                    //    Link = item.Links[0].Uri.ToString(),
                    //    Title = item.Title.Text,
                    //    PublishDate = item.PublishDate.ToString()
                    //});
                }
            }

            var channelModel = new Channel(url, feed.Title.Text, feed.Description.Text, feed.ImageUrl.ToString(), itemList, 0);
            return channelModel;
        }
    }
}
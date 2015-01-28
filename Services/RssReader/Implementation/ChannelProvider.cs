using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using AutoMapper;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelProvider : IChannelProvider
    {
        private readonly ISyndicationFeedProvider _syndicationFeedProvider;

        public ChannelProvider(ISyndicationFeedProvider syndicationFeedProvider)
        {
            _syndicationFeedProvider = syndicationFeedProvider;
        }
        
        public Channel GetChannel(string url)
        {
            var feed = _syndicationFeedProvider.GetSyndicationFeed(url);
            var channelModel = ParseFeedToChannelWithItem(url, feed);
            return channelModel;
        }

        public Channel GetChannelWithNewItems(string url, DateTime lastItemDateTime, long channelId)
        {
            var feed = _syndicationFeedProvider.GetSyndicationFeed(url);
            var channelModel = ParseFeedAndAddNewItemsToChannelWithItems(url, feed, lastItemDateTime, channelId);
            return channelModel;
        }

        private static Channel ParseFeedToChannelWithItem(string url, SyndicationFeed feed)
        {
            var itemList = GetItemsFromFeed(feed);

            return new Channel(url,
                feed.Title.Text,
                feed.Description.Text,
                feed.ImageUrl.ToString(),
                itemList);

        }

        private static List<Item> GetItemsFromFeed(SyndicationFeed feed)
        {
            var itemList = new List<Item>();

            if (feed != null)
            {
                foreach (SyndicationItem syndicationItem in feed.Items)
                {
                    var item = Mapper.Map<SyndicationItem, Item>(syndicationItem);
                    itemList.Add(item);
                }
            }

            return itemList;
        }

        private static Channel ParseFeedAndAddNewItemsToChannelWithItems(string url,SyndicationFeed feed,
                                                                        DateTime lastItemDateTime,long channelId)
        {
            var itemList = GetNewItemsFromFeed(feed, lastItemDateTime,channelId);

            return new Channel(channelId,url,feed.Title.Text,feed.Description.Text,feed.ImageUrl.ToString(),itemList);

        }

        private static List<Item> GetNewItemsFromFeed(SyndicationFeed feed, DateTime lastItemDateTime, long channelId)
        {
            var itemList = new List<Item>();

            if (feed != null)
            {
                foreach (SyndicationItem syndicationItem in feed.Items)
                {
                    if (syndicationItem.PublishDate <= lastItemDateTime)
                    {
                        break;
                    }

                    var item = Mapper.Map<SyndicationItem, Item>(syndicationItem);

                    item.ChannelId = channelId;
                    itemList.Add(item);
                }
            }

            return itemList;
        }
        

    }
}
﻿using System.Collections.Generic;
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
        
        private static Channel ParseFeedToChannelWithItem(string url, SyndicationFeed feed)
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

            var channelModel = new Channel(url, feed.Title.Text, feed.Description.Text, feed.ImageUrl.ToString(), itemList, 0);
            return channelModel;
        }
    }
}
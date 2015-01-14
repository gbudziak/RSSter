using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Models.RSS;
using Moq;
using Services.RssReader.Implementation;

namespace RSSter.Tests.Models
{
    [TestFixture]
    class TChannelList
    {

        //[Test]
        //public void T001_Given_ChannelList_And_newRssFeed_Adds_newRssFeed_to_ChannelList()
        //{
        //    // arrange
        //    var newRssFeed = new Channel("http://www.tvn24.pl/pogoda,7.xml");
        //    var cut = new ChannelList();
        //    var channelService = new ChannelService();

        //    // arrange-mock

        //    // act        
        //    channelService.AddChannel(cut, newRssFeed);

        //    // assert            
        //    Assert.AreEqual(newRssFeed, cut.Channels.FirstOrDefault(x => x == newRssFeed));

        //    // assert-mock
        //}


        [Test]
        public void T002_Given_ChannelList_and_RssFeed_Removes_RssFeed_from_ChannelList()
        {
            // arrange
            var rssFeed = "http://www.tvn24.pl/pogoda,7.xml";            
            var cut = new ChannelService();
            var stubChannelList = new ChannelList();
            stubChannelList.Channels.Add(new Channel {Items = new List<Item>(), Link = rssFeed});

            // arrange-mock            

            // act
            cut.RemoveChannel(stubChannelList, rssFeed);

            // assert
            Assert.AreEqual(0, stubChannelList.Channels.Count);

            // assert-mock
        }

    }
    
}

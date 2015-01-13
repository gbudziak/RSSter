using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Models.RSS;

namespace RSSter.Tests.Models
{
    [TestFixture]
    class TChannelList
    {        

        [Test]
        public void T001_Given_ChannelList_And_newRssFeed_Adds_newRssFeed_to_ChannelList()
        {
            // arrange
            var newRssFeed = "http://www.tvn24.pl/pogoda,7.xml";
            var cut = new ChannelList();
            var channelManager = new ChannelManager();

            // arrange-mock

            // act        
            channelManager.AddChannel(cut, newRssFeed);

            // assert            
            Assert.AreEqual(newRssFeed, cut.Channels.Find(x => x.Link == newRssFeed).ToString());

            // assert-mock
        }

        //[Test]
        //public void T002_
    }

    class ChannelManager
    {
        public void AddChannel(ChannelList list, string newRssFeed)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Services.RssReader.Implementation
{
    public interface ISyndicateFeedProvider
    {
        SyndicationFeed GetSyndicationFeed(string url);
    }

    class SyndicateFeedProvider : ISyndicateFeedProvider
    {
        public SyndicationFeed GetSyndicationFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            return feed;
        }
    }


    public interface IDateTimeAdapter
    {
        DateTime Now { get; }
    }


    internal class PgsTimePrived : IDateTimeAdapter
    {
        public DateTime Now
        {
            get { return PgsCommonTime.Now; }
        }
    }

    public class MockeDateTimeAdapter : IDateTimeAdapter
    {
        public DateTime Now
        {
            get { return new DateTime(2000, 1, 1); }
        }
    }

    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
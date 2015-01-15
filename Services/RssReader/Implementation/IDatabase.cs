using System.Collections.Generic;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public interface IDatabase
    {
        List<Channel> Channels { get; }
        List<Item> Items { get; }
    }

    public class FakeDb : IDatabase
    {
        static private List<Channel> _channels;
        static private List<Item> _items;

        public List<Channel> Channels
        {
            get { return _channels; }
            private set { _channels = value; }
        }

        public List<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }

    public class Database : IDatabase
    {
        public List<Channel> Channels { get; set; };
        public List<Item> Items { get; set; }
    }
}
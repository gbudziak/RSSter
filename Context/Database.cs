using System.Collections.Generic;
using Models.RSS;

namespace DBContext
{
    public class Database : IDatabase
    {
        //static private List<ChannelList> _channelLists;
        static private List<Channel> _channels;
        //static private List<Item> _items;


        //public List<ChannelList> ChannelLists
        //{
        //    get { return _channelLists; }
        //    set { _channelLists = value; }
        //}

        public List<Channel> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        //public List<Item> Items
        //{
        //    get { return _items; }
        //    set { _items = value; }
        //}

        static Database()
        {
            _channels = new List<Channel>(); 
        }
    }
}
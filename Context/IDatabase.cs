using System.Collections.Generic;
using Models.RSS;

namespace DBContext
{
    public interface IDatabase
    {
        //List<ChannelList> ChannelLists { get; set; }
        List<Channel> Channels { get; set; }
        //List<Item> Items { get; set; }

    }

    //public class FakeDb : IDatabase
    //{
    //    private static List<Channel> _channels;
    //    private static List<Item> _items;
    //    private static List<ChannelList> _channelLists;


    //    public List<Channel> Channels
    //    {
    //        get { return _channels; }
    //        private set { _channels = value; }
    //    }

    //    public List<Item> Items
    //    {
    //        get { return _items; }
    //        set { _items = value; }
    //    }

    //    public List<ChannelList> ChannelLists
    //    {
    //        get { return _channelLists; }
    //        set { _channelLists = value; }
    //    }
    //}
}

   
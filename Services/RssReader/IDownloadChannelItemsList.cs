using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace Services.RssReader
{
    public interface IDownloadChannelItemsList
    {
        Channel GetRssFeed(string blogUrl);
    }
}

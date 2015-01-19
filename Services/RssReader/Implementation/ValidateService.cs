using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;

namespace Services.RssReader.Implementation
{
    public class ValidateService : IValidateService
    {
        private readonly IDatabase _rssDatabase;
        private readonly IDownloadChannelItemsList _downloadChannelItemsList;

        public ValidateService(IDatabase rssDatabase, IDownloadChannelItemsList downloadChannelItemsList)
        {
            _rssDatabase = rssDatabase;
            _downloadChannelItemsList = downloadChannelItemsList;
        }
        
        public bool IsLinkUnique(string link)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Link == link).ToList();
            return !linkCount.Any();
            //var isUnique = _rssDatabase.Channels.Exists(foo => foo.Link == link);
            //return isUnique;
        }
        
        public bool IsLinkValid(string link)
        {
            try
            {
                var linkValidation = _downloadChannelItemsList.GetRssChannel(link);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

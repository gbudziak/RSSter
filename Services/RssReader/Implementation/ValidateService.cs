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

        /// <summary>
        /// Method that checks if the given link is already on channel list.
        /// </summary>
        /// <param name="link">Rss channel URL that is to be checked</param>
        /// <returns>bool, true if link is not on channel list</returns>
        public bool IsLinkUnique(string link)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Link == link).ToList();
            return !linkCount.Any();
        }

        /// <summary>
        /// Method that checks if the given link is a valid rss channel.
        /// </summary>
        /// <param name="link">Rss channel URL that is to be checked</param>
        /// <returns>bool, true if link is valid</returns>
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

        public bool IsLinkExist(string link)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Link == link).ToList();
            return !linkCount.Any();
        }

    }
}

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

        public ValidateService(IDatabase rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public bool IsLinkUnique(string link)
        {
            var linkCount = _rssDatabase.Channels.Select(foo => foo.Link == link);
            return !linkCount.Any();
        }
    }
}

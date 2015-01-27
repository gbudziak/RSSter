using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.RssReader
{
    public interface IValidateService
    {
        bool IsUrlUniqueInChannels(string url);
        bool IsUrlUniqueInUserChannels(string userId, string url);
        bool IsUrlExist(string url);
        bool IsUrlValid(string url);
        bool AddChannelRemoteValidation(string url,string userId);
    }
}

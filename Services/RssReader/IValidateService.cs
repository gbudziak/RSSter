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
        bool IsLinkUnique(string link);

        bool IsLinkValid(string link);

    }
}

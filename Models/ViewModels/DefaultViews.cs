using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.User;

namespace Models.ViewModels
{
    public static class DefaultViews
    {
        private static UserCustomView _simple = new UserCustomView
        {
            Description = false,
            Image = false,
            Rating = false,
            PublishDate = true,
            Title = true
        };

        private static UserCustomView _full = new UserCustomView
        {
            Description = true,
            Image = true,
            Rating = true,
            PublishDate = true,
            Title = true
        };

        public static UserCustomView Simple
        {
            get { return _simple; }
        }

        public static UserCustomView Full
        {
            get { return _full; }

        }
    }
}

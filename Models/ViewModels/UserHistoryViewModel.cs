namespace Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using global::Models.RSS;

    public class UserHistoryViewModel
    {
        public DateTime Date { get; set; }
        public int MyProperty { get; set; }

        public List<UserChannel> UserChannels { get; set; }

        public List<UserItem> UserItems { get; set; }
    }
}

using Models.RSS;
using Models.User;

namespace Models.ViewModels
{
    public class UserItemViewModel
    {
        public UserCustomView UserCustomView { get; set; }
        public UserItem UserItem { get; set; }

        public UserItemViewModel():this(null,null)
        { }

        public UserItemViewModel(UserCustomView view, UserItem userItem)
        {
            this.UserCustomView = view;
            this.UserItem = userItem;
        }
    }
}

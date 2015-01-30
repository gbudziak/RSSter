using Models.RSS;
using Models.User;

namespace Models.ViewModels
{
    public class UserItemsViewModel
    {
        public UserCustomView UserCustomView { get; set; }
        public UserItem UserItem { get; set; }

        public UserItemsViewModel():this(null,null)
        { }

        public UserItemsViewModel(UserCustomView view, UserItem userItem)
        {
            this.UserCustomView = view;
            this.UserItem = userItem;
        }
    }
}

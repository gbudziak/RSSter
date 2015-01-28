using Models.RSS;
using Models.User;

namespace Models.ViewModels
{
    public class UserItemView
    {
        public UserCustomView UserCustomView { get; set; }
        public UserItem UserItem { get; set; }

        public UserItemView():this(null,null)
        { }

        public UserItemView(UserCustomView view, UserItem userItem)
        {
            this.UserCustomView = view;
            this.UserItem = userItem;
        }
    }
}

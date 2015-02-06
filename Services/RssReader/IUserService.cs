using Models.User;

namespace Services.RssReader
{
    public interface IUserService
    {
        void SetUserCustomView(UserCustomView userCustomView);
    }
}
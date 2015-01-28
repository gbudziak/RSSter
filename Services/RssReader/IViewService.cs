using Models.User;

namespace Services.RssReader
{
    public interface IViewService
    {
        UserCustomView FetchView(string userId);
    }
}
using System;

namespace RssDataContext
{
    public interface IRssTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
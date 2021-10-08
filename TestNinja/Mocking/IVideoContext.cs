using System.Data.Entity;

namespace TestNinja.Mocking
{
    public interface IVideoContext
    {
        DbSet<Video> Videos { get; set; }
    }
}
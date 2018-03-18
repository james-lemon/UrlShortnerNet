using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrlShortenerNet.Models;

namespace UrlShortenerNet
{
    public class UrlShortnerContext : DbContext
    {
        public DbSet<Redirect> Redirects { get; set; }
        public DbSet<ExpiredRedirect> ExpiredRedirects { get; set; }
        public DbSet<BadEntryPointRedirect> BadEntryPoints { get; set; }

        public ExpiredRedirect GetExpiredRedirect()
        {
            return ExpiredRedirects.OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }

        public BadEntryPointRedirect BadEntryPointRedirect()
        {
            return BadEntryPoints.OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }

        public UrlShortnerContext(DbContextOptions<UrlShortnerContext> options) : base(options)
        {
        }
    }
}

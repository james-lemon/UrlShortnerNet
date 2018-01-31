using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerNet.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace UrlShortenerNet
{
    public class UrlShortnerContext : DbContext
    {
        public DbSet<Redirect> Redirects { get; set; }

        public UrlShortnerContext(DbContextOptions<UrlShortnerContext> options) : base(options)
        {

        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortenerNet.Models
{
    public class BadEntryPointRedirect
    {
        [Key]
        public int BadEntryPointRedirectId { get; set; }
        public string EndPoint { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}

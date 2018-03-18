using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortenerNet.Models
{
    public class ExpiredRedirect
    {
        [Key]
        public int ExpiredRedirectId { get; set; }
        public string EndPoint { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}

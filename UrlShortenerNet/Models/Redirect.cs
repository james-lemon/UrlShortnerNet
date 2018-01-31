using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UrlShortenerNet.Models
{
    public class Redirect 
    {
        [Key]
        public int RedirectId { get; set; }
        public string EndPoint { get; set; }
        public string EntryPoint { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public void CreateEntryPoint(int charLenght = 7)
        {
            //TODO add check if entry point doesn't already exit
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            this.EntryPoint = new string (Enumerable.Repeat(chars, charLenght)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
}
}

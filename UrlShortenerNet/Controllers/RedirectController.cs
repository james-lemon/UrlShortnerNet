using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerNet.Models;

namespace UrlShortenerNet.Controllers
{
    [Produces("application/json")]
    [Route("api/Redirect")]
    public class RedirectController : Controller
    {
        private UrlShortnerContext _context;
        public RedirectController(UrlShortnerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Redirect Redirect(Redirect redirect)
        {
            redirect.CreateDate = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(redirect.EndPoint))
            {
                throw new ArgumentException("Value EndPoint must be populated with a valid URL");
            }
            if (string.IsNullOrWhiteSpace(redirect.EntryPoint))
            {
                redirect.CreateEntryPoint();
            }
            else if (_context.Redirects.Any(y => String.Equals(y.EntryPoint, redirect.EntryPoint, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new Exception("Entry Point already claimed");
            }
            _context.Redirects.Add(redirect);
            _context.SaveChangesAsync();
            return redirect;
        }

        
        [HttpGet]
        public IActionResult Redirect(string entryPoint)
        {
            var redirect = _context.Redirects.FirstOrDefault(x => x.EntryPoint.Equals(entryPoint, StringComparison.CurrentCultureIgnoreCase));
            if(redirect != null)
            {
                return View(redirect);
            }
            else
            {
                throw new Exception("Webpage not found");
            }
        }

    }
}
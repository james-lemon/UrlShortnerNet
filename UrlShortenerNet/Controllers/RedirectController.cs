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
        public IActionResult Redirect(Redirect redirect)
        {
            redirect.CreateDate = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(redirect.EndPoint))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            if (string.IsNullOrWhiteSpace(redirect.EntryPoint))
            {
                redirect.CreateEntryPoint();
            }
            else if (_context.Redirects.Any(y => String.Equals(y.EntryPoint, redirect.EntryPoint, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            _context.Redirects.Add(redirect);
            _context.SaveChangesAsync();
            return Json(redirect);
        }

        [HttpPost]
        public IActionResult BadEntryRedirect(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var badEntryRedirect = new BadEntryPointRedirect();
            badEntryRedirect.CreateDate = DateTime.UtcNow;
            badEntryRedirect.EndPoint = endpoint;
            _context.BadEntryPoints.Add(badEntryRedirect);
            _context.SaveChangesAsync();
            return Json(badEntryRedirect);
        }


        [HttpGet]
        public IActionResult Redirect(string entryPoint)
        {
            var redirect = _context.Redirects.FirstOrDefault(x => x.EntryPoint.Equals(entryPoint, StringComparison.CurrentCultureIgnoreCase));
            if (redirect != null)
            {
                return View(redirect);
            }
            if (_context.BadEntryPoints.Any())
            {
                redirect = new Redirect
                {
                    EndPoint = _context.BadEntryPoints.OrderByDescending(x => x.CreateDate).FirstOrDefault().EndPoint
                };
                return View(redirect);
            }
            else
            {
                throw new Exception("Webpage not found");
            }
        }
    }
}
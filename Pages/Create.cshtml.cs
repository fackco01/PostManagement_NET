using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment3.ApplicationContext;
using Assignment3.Model;
using Assignment3.HubServer;
using Microsoft.AspNetCore.SignalR;

namespace Assignment3.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Assignment3.ApplicationContext.PostAppContext _context;

        private readonly IHubContext<SignalRHub> _hub;
        public CreateModel(Assignment3.ApplicationContext.PostAppContext context, IHubContext<SignalRHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return Page();
        }

        [BindProperty]
        public Posts Posts { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Posts == null || Posts == null)
            {

                return Page();
            }

            _context.Posts.Add(Posts);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("LoadPost");

            return RedirectToPage("./Index");
        }
    }
}

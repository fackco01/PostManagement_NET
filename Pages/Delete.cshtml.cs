using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment3.ApplicationContext;
using Assignment3.Model;
using Microsoft.AspNetCore.SignalR;
using Assignment3.HubServer;

namespace Assignment3.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment3.ApplicationContext.PostAppContext _context;
        private readonly IHubContext<SignalRHub> _hub;
        public DeleteModel(Assignment3.ApplicationContext.PostAppContext context, IHubContext<SignalRHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [BindProperty]
      public Posts Posts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);

            if (posts == null)
            {
                return NotFound();
            }
            else 
            {
                Posts = posts;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }
            var posts = await _context.Posts.FindAsync(id);

            if (posts != null)
            {
                Posts = posts;
                _context.Posts.Remove(Posts);
                await _context.SaveChangesAsync();
                await _hub.Clients.All.SendAsync("LoadPost");
            }

            return RedirectToPage("./Index");
        }
    }
}

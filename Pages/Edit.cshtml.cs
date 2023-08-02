using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.ApplicationContext;
using Assignment3.Model;
using Microsoft.AspNetCore.SignalR;
using Assignment3.HubServer;

namespace Assignment3.Pages
{
    public class EditModel : PageModel
    {
        private readonly Assignment3.ApplicationContext.PostAppContext _context;
        private readonly IHubContext<SignalRHub> _hub;
        public EditModel(Assignment3.ApplicationContext.PostAppContext context, IHubContext<SignalRHub> hub)
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

            var posts =  await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);
            if (posts == null)
            {
                return NotFound();
            }
            Posts = posts;
           ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
           ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            System.Diagnostics.Debug.WriteLine(Posts.Content);
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(Posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _hub.Clients.All.SendAsync("LoadPost");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(Posts.PostID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostsExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}

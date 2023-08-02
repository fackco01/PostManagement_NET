using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment3.ApplicationContext;
using Assignment3.Model;

namespace Assignment3.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment3.ApplicationContext.PostAppContext _context;

        public DetailsModel(Assignment3.ApplicationContext.PostAppContext context)
        {
            _context = context;
        }

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
    }
}

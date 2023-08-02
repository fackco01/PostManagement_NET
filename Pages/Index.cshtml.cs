using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment3.ApplicationContext;
using Assignment3.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Assignment3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Assignment3.ApplicationContext.PostAppContext _context;

        public IndexModel(Assignment3.ApplicationContext.PostAppContext context)
        {
            _context = context;
        }

        public IList<Posts> Posts { get; set; } = default!;

        public async Task<JsonResult> OnGetPost()
        {
            if (_context.Posts != null)
            {
                Posts = await _context.Posts
                .Include(p => p.category)
                .Include(p => p.user).ToListAsync();
            }

            return new JsonResult(Posts);
        }

        public async Task<JsonResult> OnGetSearching(string search, string type)
        {
            if (_context.Posts != null && !search.IsNullOrEmpty())
            {
                if (Regex.IsMatch(search, @"\d+"))
                {
                    Posts = await _context.Posts.Where(p => p.PostID == Convert.ToInt32(search))
                            .Include(p => p.category)
                            .Include(p => p.user).ToListAsync();
                }
                else
                {
                    if (type == "title")
                    {
                        Posts = await _context.Posts.Where(p => p.Title.Contains(search))
                                .Include(p => p.category)
                                .Include(p => p.user).ToListAsync();
                    }
                    else
                    {
                        Posts = await _context.Posts.Where(p => p.category.CategoryDescription.Contains(search))
                                .Include(p => p.category)
                                .Include(p => p.user).ToListAsync();
                    }
                }

            }

            return new JsonResult(Posts);
        }


    }
}

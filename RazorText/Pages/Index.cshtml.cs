using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorText.Data;

namespace RazorText.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SqlDbContext _db;

        [BindProperty]
        public Article Article { get; private set; }

        public IndexModel(SqlDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {          
            Article = new Article
            {
                CreateDate = DateTime.Now,
                Text = "Enter new text",
                Title = "Create new article",
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Article = _db.CreateNewArticle(Article);

            return RedirectToPage("Index", $"/{Article.UniqueTitle}");
        }        
    }
}

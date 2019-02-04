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
        [BindProperty(SupportsGet = true)]
        public string UniqueTitle { get; set; }

        public IndexModel(SqlDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(UniqueTitle))
                Article = _db.Articles.Where(a => a.UniqueTitle == UniqueTitle).FirstOrDefault();

            if(Article == null)
                Article = new Article
                {
                    CreateDate = DateTime.Now,
                    Text = "Enter new text",
                    Title = "Create new article",
                    UniqueTitle = "create-new"
                };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Article.UniqueTitle = _db.GetUniqueTitle(Article.Title);
            _db.Articles.Add(Article);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index", $"/{Article.UniqueTitle}");
        }        
    }
}

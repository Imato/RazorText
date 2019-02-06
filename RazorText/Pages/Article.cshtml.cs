using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorText.Data;

namespace RazorText.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly SqlDbContext _db;
        private readonly ILogger<ArticleModel> _logger;

        [BindProperty]
        public Article Article { get; private set; }
        [BindProperty(SupportsGet = true)]
        public string UniqueTitle { get; set; }

        public ArticleModel(SqlDbContext db, ILogger<ArticleModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(UniqueTitle))
                Article = _db.Articles.Where(a => a.UniqueTitle == UniqueTitle).FirstOrDefault();

            if (Article == null)
                RedirectToPage("Index");
        }
    }
}
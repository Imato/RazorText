using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RazorText.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var db = new SqlDbContext(serviceProvider.GetRequiredService<DbContextOptions<SqlDbContext>>()))
            {
                if (db.Articles.Any())
                    return;

                db.Articles.Add(new Article
                {
                    Author = "Imato",
                    CreateDate = DateTime.Now,
                    Tags = "Test",
                    Text = "Same new article",
                    Title = "Same new article",
                    UniqueTitle = db.GetUniqueTitle("Same new article")
                });

                db.SaveChanges();
            }
        }

        
    }
}

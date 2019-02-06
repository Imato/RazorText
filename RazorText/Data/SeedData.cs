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

                db.CreateNewArticle("Same new article", "Imato", "Same new article", "Test");

                var a2 = new Article
                {
                    Author = "Imato",
                    CreateDate = DateTime.Now,
                    Tags = "Test;Markdown",
                    Text = @"#### This is Markdown text inside of a Markdown block

    * Item 1
    * Item 2
 
    ### Dynamic Data is supported:
    The current Time is: @DateTime.Now.ToString(""HH:mm:ss"")

    ```cs
    // this c# is a code block
    for (int i = 0; i < lines.Length; i++)
                {
                    line1 = lines[i];
                    if (!string.IsNullOrEmpty(line1))
                        break;
                }
    ```",
                    Title = "Text with markdown"
                };

                db.CreateNewArticle(a2);
                db.SaveChanges();
            }
        }

        
    }
}

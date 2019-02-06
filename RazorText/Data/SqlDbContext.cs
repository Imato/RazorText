using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorText.Data
{
    public class SqlDbContext : IdentityDbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {            

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }

        private string GetUniqueTitleOld(string title, int count = 0)
        {
            var length = title.Length < 155 ? title.Length : 155;
            var unique = title.Replace(" ", "-").Substring(0, length).ToLower();
            unique = count > 0 ? $"{unique}-{count}" : unique;

            if (Articles.Where(a => a.UniqueTitle == unique).Count() > 0)
                unique = GetUniqueTitleOld(unique, count++);

            return unique;
        }

        private string GetUniqueTitle(Article article)
        {
            var length = article.Title.Length < 155 ? article.Title.Length : 155;
            var unique = article.Title.Replace(" ", "-").Substring(0, length).ToLower();

            if (Articles.Where(a => a.UniqueTitle == unique).Count() > 0)
                unique = $"{unique}-{article.CreateDate.ToString("yyyy-MM-dd-HH:mm:ss")}";
   
            return unique;
        }

        public Article CreateNewArticle(Article article)
        {
            return CreateNewArticle(article.Title, article.Author, article.Text, article.Tags);
        }

        public Article CreateNewArticle(string title, string author, string text, string tags)
        {
            var article = new Article
            {
                Author = author,
                CreateDate = DateTime.Now,
                Tags = tags,
                Text = text,
                Title = title
            };

            article.UniqueTitle = GetUniqueTitle(article);
            Articles.Add(article);
            SaveChanges();

            article = Articles.Where(a => a.UniqueTitle == article.UniqueTitle).FirstOrDefault();
            return article;
        }
    }
}

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

        public string GetUniqueTitle(string title, int count = 0)
        {
            var length = title.Length < 155 ? title.Length : 155;
            var unique = title.Replace(" ", "-").Substring(0, length).ToLower();
            unique = count > 0 ? $"{unique}-{count}" : unique;

            if (Articles.Where(a => a.UniqueTitle == unique).Count() > 0)
                unique = GetUniqueTitle(unique, count++);

            return unique;
        }
    }
}

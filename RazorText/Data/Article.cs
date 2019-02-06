using System;
using System.ComponentModel.DataAnnotations;

namespace RazorText.Data
{
    public class Article
    {
        public int Id { get; set; }
        [Required,MaxLength(155)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDate { get; set; }
        public string UniqueTitle { get; set; }
    }
}

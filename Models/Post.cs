using System;
using System.ComponentModel.DataAnnotations;

namespace IlaydaTechBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "İçerik alanı zorunludur.")]
        public string Content { get; set; } = string.Empty;

        [StringLength(100)]
        public string Author { get; set; } = "İlayda";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
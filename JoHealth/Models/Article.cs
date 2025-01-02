namespace JoHealth.Models
{
    public class Article
    {
        public int Id { get; set; } // Unique identifier for the article
        public string Title { get; set; } // Title of the article
        public string Body { get; set; } // Full text content of the article
        public string ImageUrl { get; set; } // URL or path to the article's image
        public DateTime PublishDate { get; set; } // Date when the article was published
        public string Author { get; set; } // Author of the article

        // Optional: Method to get a short preview of the article
        public string GetPreview(int characterLimit = 100)
        {
            if (Body.Length <= characterLimit) return Body;
            return Body.Substring(0, characterLimit) + "...";
        }
    }
}

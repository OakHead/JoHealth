namespace JoHealth.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public string GetPreview(int characterLimit = 100)
        {
            if (Body.Length <= characterLimit) return Body;
            return Body.Substring(0, characterLimit) + "...";
        }
    }
}

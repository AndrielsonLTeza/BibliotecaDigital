namespace BibliotecaDigital.Core.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        // Navigation property for one-to-many relationship
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

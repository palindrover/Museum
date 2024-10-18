namespace Museum.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IEnumerable<Exhibit> Exhibits { get; set; }
        public string[]? Leadup { get; set; }

    }
}

namespace Museum.Models
{
    public class Exhibit
    {
        public int Id { get; set; }
        public int ExpositionId { get; set; }
        public int CategoryId { get; set; }
        public int ExhibitionHallId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string[]? Images { get; set; }
        public int IsTransmitted { get; set; }
        public int WhereTransmittedId { get; set; }
        public string? InvNum { get; set; }
    }
}

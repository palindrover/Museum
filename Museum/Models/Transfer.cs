namespace Museum.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int Contractor {  get; set; }
        public string? Sender { get; set; }
        public string? Address { get; set; }
        public string? Date {  get; set; }
        public string? Returns { get; set; }
        public string? Purpose { get; set; }
        public string? Docnum { get; set; }
        public int[]? Contractors { get; set; }
        public int[]? Exhibits { get; set; }
    }
}

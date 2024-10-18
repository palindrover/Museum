using Museum.Contexts;

namespace Museum.Models
{
    public class Transfer
    {
        private TransferContext _context;
        public int ID { get; set; }
        public string ExhibTitle { get; set; }
        public int ExhibInvNum { get; set; }
        public int? Contractor { get; set; }
        public string? Sender { get; set; }
        public string? TransferDate { get; set; }
        public string? Returns { get; set; }
        public string? Purpose { get; set; }
        public string? DocNum { get; set; }
        public string? Address { get; set; }
    }
}

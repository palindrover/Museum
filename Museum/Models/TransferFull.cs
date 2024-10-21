using Museum.Contexts;

namespace Museum.Models
{
    public class TransferFull
    {
        private TransferContext _context;
        public int Id { get; set; }
        public string ExhibTitle { get; set; }
        public int ExhibInvNum { get; set; }
        public string ExhibImage { get; set; }
        public string Sender { get; set; }
        public string TransferDate { get; set; }
        public string Returns { get; set; }
        public string Purpose { get; set; }
        public string DocNum { get; set; }
        public string ContrSurname { get; set; }
        public string ContrName { get; set; }
        public string ContrPatrName { get; set; }
        public string ContrComp { get; set; }
        public string ContrTel { get; set; }
        public string ContrMail { get; set; }
        public string Address { get; set; }
    }
}

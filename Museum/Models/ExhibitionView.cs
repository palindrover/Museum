using Museum.Models;

namespace Museum.Models
{
    public class ExhibitionView
    {
        public Exhibition Exhibition { get; set; }
        public Exhibit[] Exhibits { get; set; }
    }
}

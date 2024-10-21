namespace Museum.Models
{
	public class AddTransfer(IEnumerable<Exhibit> exhibits, IEnumerable<Contractor> contractors)
	{
		public IEnumerable<Exhibit> Exhibits { get; set; } = exhibits;
		public IEnumerable<Contractor> Contractors { get; set; } = contractors;
	}
}

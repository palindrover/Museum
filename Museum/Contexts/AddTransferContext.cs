using Museum.Models;

namespace Museum.Contexts
{
	public class AddTransferContext(string connectionString) : BaseContext(connectionString)
	{
		public AddTransfer GetData(IEnumerable<Exhibit> exhibits, IEnumerable<Contractor> contractors)
		{
			return new AddTransfer(exhibits, contractors);
		}
	}
}

using Museum.Models;

namespace Museum.Contexts
{
    public class EditHallContext(string connectionString) : BaseContext(connectionString)
    {
        public EditHall GetData(Hall hall, IEnumerable<MyFile> images) 
        {
            return new EditHall {Hall = hall, Images = images };
        }
    }
}

using Museum.Models;

namespace Museum.Contexts
{
    public class AddExhibitContext(string connectionString) : BaseContext(connectionString)
    {
        public AddExhibit GetData(IEnumerable<Hall> halls, IEnumerable<Category> categories, IEnumerable<MyFile> files)
        {
            return new AddExhibit { Halls = halls, Categories = categories, Images = files};
        }
    }
}

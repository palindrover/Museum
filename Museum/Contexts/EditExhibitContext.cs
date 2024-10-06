using Museum.Models;
using MySqlX.XDevAPI.Common;

namespace Museum.Contexts
{
    public class EditExhibitContext(string connectionString) : BaseContext(connectionString)
    {
        public EditExhibit GetData(Exhibit exhibit, IEnumerable<Hall> halls, IEnumerable<Category> categories, IEnumerable<MyFile> images)
        {
            var result = new EditExhibit { Result = exhibit, Halls = halls, Categories = categories, Images = images };
            return result;
        }
    }
}
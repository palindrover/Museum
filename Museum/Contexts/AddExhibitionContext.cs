using Museum.Models;

namespace Museum.Contexts
{
    public class AddExhibitionContext(string connectionString) : BaseContext(connectionString)
    {
        public AddExhibition GetData(IEnumerable<Exhibit> exhibits, IEnumerable<MyFile> images)
        {
            return new AddExhibition (exhibits, images);
        }
    }
}

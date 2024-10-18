namespace Museum.Models
{
    public class AddExhibition(IEnumerable<Exhibit> exhibits, IEnumerable<MyFile> images)
    {
        public IEnumerable<Exhibit> Exhibits { get; set; } = exhibits;
        public IEnumerable<MyFile> Images { get; set; } = images;
    }
}

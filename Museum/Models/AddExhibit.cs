namespace Museum.Models
{
    public class AddExhibit
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hall> Halls { get; set; }
        public IEnumerable<MyFile> Images { get; set; }
    }
}

namespace Museum.Models
{
    public class EditExhibit
    {
        public Exhibit Result { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hall> Halls { get; set; }
        public IEnumerable<MyFile> Images { get; set; }
    }
}

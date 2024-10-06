namespace Museum.Models
{
    public class EditHall
    {
        public Hall Hall { get; set; }
        public IEnumerable<MyFile> Images { get; set; }
    }
}

using Microsoft.Extensions.Hosting.Internal;
using Museum.Models;

namespace Museum.Contexts
{
    public class FileContext(string connectionString) : BaseContext(connectionString)
    {
        private readonly string _path = "wwwroot\\Files";
        internal IEnumerable<MyFile> GetData()
        {
            var files = Directory.GetFiles(_path);
            var result = new List<MyFile>();
            foreach (var file in files)
            {
                var temp = file.Split('\\');
                result.Add(new MyFile() { Path = $"\\Files\\{temp.Last()}" });
            }
            return result;
        }
    }
}

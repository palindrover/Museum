using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class CategoryContext : BaseContext
    {
        public CategoryContext(string connectionString) : base(connectionString) { }

        private List<Category> _list;

        private void CheckListClear()
        {
            if(_list ==  null) _list = new List<Category>();
            else _list.Clear();
        }

        private void MySQGetResult(string command)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(command, conn);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _list.Add(new Category()
                    {
                        Id = _sd.SafeGetNumericData(reader, "id"),
                        Name = _sd.SafeGetStringData(reader, "exhibitioncategory"),
                        Description = _sd.SafeGetStringData(reader, "exhibitioncategorydescription"),
                        Image = _sd.SafeGetStringData(reader, "exhibitioncategoryimage")
                    });
                }
            }
        }

        public List<Category> GetCategories()
        {
            CheckListClear();

            MySQGetResult("SELECT * FROM exhibitioncategory");

            return _list;
        }
    }
}

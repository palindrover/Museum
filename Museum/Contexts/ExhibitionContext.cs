using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class ExhibitionContext : BaseContext
    {
        private List<Exhibition> _list;

        public ExhibitionContext(string connectionString) : base(connectionString) { }

        private void MySQGetResult(string command)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(command, conn);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _list.Add(new Exhibition()
                    {
                        Id = _sd.SafeGetNumericData(reader, "id"),
                        Title = _sd.SafeGetStringData(reader, "exhibitiontitle"),
                        Description = _sd.SafeGetStringData(reader, "exhibitiondescription"),
                        Image = _sd.SafeGetStringData(reader, "exhibitionimage"),
                        Exhibits = _sd.SafeGetStringData(reader, "exhibitsarray").Split(',').Select(x => int.Parse(x)).ToArray(),
                        Leadup = _sd.SafeGetStringData(reader, "exhibitsleadup")
                    });
                }
            }
        }

        private void CheckExhibitionListClear()
        {
            if (_list == null) _list = new List<Exhibition>();
            else _list.Clear();
        }

        public List<Exhibition> GetAllExhibitions()
        {
            CheckExhibitionListClear();

            MySQGetResult("SELECT * FROM exhibitions");

            return _list;
        }

        public Exhibition GetById(int id)
        {
            if (_list == null) GetAllExhibitions();

            return _list.Find(el => el.Id == id);
        }
    }
}
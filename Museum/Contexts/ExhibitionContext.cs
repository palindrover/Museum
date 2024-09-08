using MySql.Data.MySqlClient;
using Museum.Models;
using Org.BouncyCastle.Security;

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
                        Leadup = _sd.SafeGetStringData(reader, "exhibitsleadup").Split('#')
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

        public Exhibition GetExhibitionById(int id)
        {
            var result = new Exhibition();
            string Exhibits = string.Empty;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM exhibitions INNER JOIN exhibits ON exhibitions.id = exhibits.expositionid WHERE exhibits.expositionid=" + id, conn);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.Id = _sd.SafeGetNumericData(reader, "id");
                    result.Title = _sd.SafeGetStringData(reader, "exhibitiontitle");
                    result.Description = _sd.SafeGetStringData(reader, "exhibitiondescription");
                    result.Image = _sd.SafeGetStringData(reader, "exhibitionimage");
                    result.Leadup = _sd.SafeGetStringData(reader, "exhibitsleadup").Split('#');
                    Exhibits = _sd.SafeGetStringData(reader, "exhibitsarray");
                }
                conn.Close();
                result.Exhibits = GetExhibits(Exhibits);
            }

            return result;
        }

        private List<Exhibit> GetExhibits(string ids)
        {
            List<Exhibit> list = new List<Exhibit>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM exhibits WHERE id IN(" + ids + ")", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Exhibit()
                        {
                            Id = reader.GetInt32("id"),
                            Title = _sd.SafeGetStringData(reader, "title"),
                            Description = _sd.SafeGetStringData(reader, "description"),
                            Images = _sd.SafeGetStringData(reader, "images").Split('#'),
                            InvNum = _sd.SafeGetStringData(reader, "invnum"),
                        });
                    }
                }
                conn.Close();
                return list;
            }
        }
    }
}
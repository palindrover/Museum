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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM exhibits WHERE id IN('" + ids + "')", conn);

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

        public int Add(string title, string description, string image, string exhibits, string leadups)
        {
			var result = 0;
			using MySqlConnection conn = GetConnection();
			var ex = exhibits.First().ToString();
			for (var i = 1; i < exhibits.Count(); i++)
			{
				ex += ",";
				ex += exhibits.ElementAt(i).ToString();
			}
			var lu = leadups.First().ToString();
			for (var i = 1; i < leadups.Count(); i++)
			{
				lu += "#";
				lu += leadups.ElementAt(i).ToString();
			}
			conn.Open();
			var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO exhibitions (exhibitiontitle, exhibitiondescription, exhibitionimage, exhibitsarray, exhibitsleadup) VALUES ('" +
                title + "','" + description + "','" + image + "','" + exhibits + "','" + leadups + "')";
            cmd.ExecuteNonQuery();
			cmd.CommandText = "SELECT LAST_INSERT_ID()";
			var reader = cmd.ExecuteReader();

			if (reader.Read()) result = reader.GetInt32("LAST_INSERT_ID()");
			conn.Close();
			return result;
		}

        public void Delete(int id)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM exhibitions WHERE id=" + id;
            cmd.ExecuteNonQuery();
        }
    }
}
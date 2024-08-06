using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class HallContext : BaseContext
    {
        private List<Hall> _list;

        public HallContext(string connectionString) : base(connectionString) { }

        private void CheckHallListClear()
        {
            if(_list == null ) _list = new List<Hall>();
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
                    _list.Add(new Hall()
                    {
                        Id = _sd.SafeGetNumericData(reader, "id"),
                        HallTitle = _sd.SafeGetStringData(reader, "halltitle"),
                        HallAddress = _sd.SafeGetStringData(reader, "halladdress"),
                        HallLocation = _sd.SafeGetStringData(reader, "halllocation"),
                        HallImage = _sd.SafeGetStringData(reader, "hallimage"),
                        HallContains = _sd.SafeGetStringData(reader, "hallcontains")
                    });
                }
            }
        }

        public List<Hall> GetAllHalls()
        {
            CheckHallListClear();

            MySQGetResult("SELECT * FROM exhibitionhalls");

            return _list;
        }
    }
}

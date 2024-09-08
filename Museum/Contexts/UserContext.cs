using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class UserContext : BaseContext
    {
        public UserContext(string connectiionString) : base(connectiionString) { }

        private List<User> _list;

        private void CheckListClear()
        {
            if (_list == null) _list = new List<User>();
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
                    _list.Add(new User()
                    {
                        Id = _sd.SafeGetNumericData(reader, "id"),
                        Username = _sd.SafeGetStringData(reader, "username"),
                        Login = _sd.SafeGetStringData(reader, "login"),
                        Pass = _sd.SafeGetStringData(reader, "pass"),
                        Salt = _sd.SafeGetStringData(reader, "salt"),
                        Role = _sd.SafeGetNumericData(reader, "r"),
                    });
                }
            }
        }

        public List<User> GetAllUsers()
        {
            CheckListClear();

            MySQGetResult("SELECT * FROM users");

            return _list;
        }
    }
}

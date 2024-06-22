using MySql.Data.MySqlClient;
using System.Data;

namespace Museum.Controllers.UtilityControllers
{
    public class SafeGetDataController
    {
        public string SafeGetStringData(MySqlDataReader reader, string column)
        {
            if (!reader.IsDBNull(column)) return reader.GetString(column);
            else return String.Empty;
        }

        public int SafeGetNumericData(MySqlDataReader reader, string column)
        {
            if(!reader.IsDBNull(column)) return reader.GetInt32(column);
            else return 0;
        }
    }
}

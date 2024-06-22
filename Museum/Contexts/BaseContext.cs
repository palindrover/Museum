using MySql.Data.MySqlClient;
using Museum.Controllers.UtilityControllers;

namespace Museum.Contexts
{
    public class BaseContext
    {
        protected string ConnectionString { get; set; }
        protected SafeGetDataController _sd;

        public BaseContext(string connectionString)
        {
            ConnectionString = connectionString;
            _sd = new SafeGetDataController();
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

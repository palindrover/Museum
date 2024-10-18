using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class TransferContext : BaseContext
    {
        public TransferContext(string connectionString) : base(connectionString) { }

        public List<Transfer> GetAllTransfers()
        {
            List<Transfer> result = new List<Transfer>();
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM transfers INNER JOIN exhibits ON exhibits.wheretransmittedid = transfers.id";
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Transfer()
                {
                    ID = _sd.SafeGetNumericData(reader, "id"),
                    Contractor = _sd.SafeGetNumericData(reader, "contractor"),
                    Sender = _sd.SafeGetStringData(reader, "sender"),
                    TransferDate = _sd.SafeGetStringData(reader, "transferdate"),
                    Returns = _sd.SafeGetStringData(reader, "returns"),
                    Purpose = _sd.SafeGetStringData(reader, "purpose"),
                    DocNum = _sd.SafeGetStringData(reader, "docnum"),
                    Address = _sd.SafeGetStringData(reader,"address"),
                    ExhibInvNum = _sd.SafeGetNumericData(reader, "invnum"),
                    ExhibTitle = _sd.SafeGetStringData(reader,"title")
                });
            }
            reader.Close();
            conn.Close();
            return result;
        }

        public void Delete(int id)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM transfers WHERE id=" + id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}

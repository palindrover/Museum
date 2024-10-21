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

        public TransferFull GetFullTransfer(int id)
        {
            var result = new TransferFull();
            using MySqlConnection conn = GetConnection();
			conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT exhibits.title, exhibits.invnum, exhibits.images, transfers.id, transfers.sender, transfers.transferdate, " +
                "transfers.returns, transfers.purpose, transfers.docnum, contractors.surname, contractors.name, contractors.patrname, " +
                "contractors.companyname, contractors.tel, contractors.email, transfers.address FROM (exhibits INNER JOIN (transfers INNER JOIN" +
                " contractors ON contractors.id = transfers.contractor) ON transfers.id = exhibits.wheretransmittedid) WHERE transfers.id=" + id;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Id = _sd.SafeGetNumericData(reader, "id");
                    result.Address = _sd.SafeGetStringData(reader, "address");
                    result.DocNum = _sd.SafeGetStringData(reader, "docnum");
                    result.ContrSurname = _sd.SafeGetStringData(reader, "surname");
                    result.ContrName = _sd.SafeGetStringData(reader, "name");
                    result.ContrComp = _sd.SafeGetStringData(reader, "companyname");
                    result.ContrMail = _sd.SafeGetStringData(reader, "email");
                    result.ContrPatrName = _sd.SafeGetStringData(reader, "patrname");
                    result.ContrTel = _sd.SafeGetStringData(reader, "tel");
                    result.ExhibImage = _sd.SafeGetStringData(reader, "images").Split(' ')[0];
                    result.ExhibInvNum = _sd.SafeGetNumericData(reader, "invnum");
                    result.ExhibTitle = _sd.SafeGetStringData(reader, "title");
                    result.Purpose = _sd.SafeGetStringData(reader, "purpose");
                    result.Returns = _sd.SafeGetStringData(reader, "returns");
                    result.Sender = _sd.SafeGetStringData(reader, "sender");
                    result.TransferDate = _sd.SafeGetStringData(reader, "transferdate");
                }
            }
            conn.Close();
            return result;
        }

        public void Edit(int id, string sender, string transferDate, string returns, string docNum, string address)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE transfers SET sender='" + sender + "', transferdate='" + transferDate + "', returns='" + returns +
                "', docnum='" + docNum + "', address='" + address + "' WHERE id=" + id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int Add(string sender, string transferdate, string purpose, string returns, string docnum, string address, int contractorid)
        {
            int result = -1;

            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO transfers (sender, transferdate, purpose, returns, docnum, address, contractor) VALUES ('" +
                sender + "','" + transferdate + "','" + purpose + "','" + returns + "','" + docnum + "','" + address + "','" + contractorid + "')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            var reader = cmd.ExecuteReader();
            if(reader.Read()) result = reader.GetInt32("LAST_INSERT_ID()");

            conn.Close();
            return result;
        }
    }
}

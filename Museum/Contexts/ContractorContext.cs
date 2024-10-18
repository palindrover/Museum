using MySql.Data.MySqlClient;
using Museum.Models;

namespace Museum.Contexts
{
    public class ContractorContext : BaseContext
    {
        public ContractorContext(string connectionString) : base(connectionString) { }

        public List<Contractor> GetAllContractors()
        {
            List<Contractor> contractors = new List<Contractor>();
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM contractors";
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                contractors.Add(new Contractor()
                {
                    Id = _sd.SafeGetNumericData(reader, "id"),
                    Surname = _sd.SafeGetStringData(reader, "surname"),
                    Name = _sd.SafeGetStringData(reader,"name"),
                    Patrname = _sd.SafeGetStringData(reader, "patrname"),
                    Companyname = _sd.SafeGetStringData(reader, "companyname"),
                    Tel = _sd.SafeGetStringData(reader, "tel"),
                    Email = _sd.SafeGetStringData(reader,"email")
                });
                    
            }
            conn.Close();
            return contractors;
        }

        public void Add(string company, string surname, string name, string patrname, string tel, string email)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO contractors (companyname, surname, name, patrname, tel, email) VALUES ('" +
                company + "','" + surname + "','" + name + "','" + patrname + "','" + tel + "','" + email + "')";
            cmd.ExecuteNonQuery();
        }

        public void Edit(int id, string companyname, string surname, string name, string patrname, string tel, string email)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE contractors SET companyname='" + companyname + "',surname='" + surname + "',name='" +
                name + "',patrname='" + patrname + "',tel='" + tel + "',email='" + email + "' WHERE id=" + id;
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM contractors WHERE id=" + id;
            cmd.ExecuteNonQuery();
        }
    }
}

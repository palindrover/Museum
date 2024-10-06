using MySql.Data.MySqlClient;
using Museum.Models;
using System.Data;
using System.Linq;

namespace Museum.Contexts
{
    public class ExhibitContext : BaseContext
    {
        private List<Exhibit> _list;

        public ExhibitContext(string connectionString ) : base(connectionString) { }

        private void CheckExhibitListClear()
        {
            if (_list == null) _list = new List<Exhibit>();
            else _list.Clear();
        }

        public List<Exhibit> GetAllExhibits()
        {
            CheckExhibitListClear();

            MySQGetResult("SELECT * FROM exhibits");

            return _list;
        }

        public List<Exhibit> GetByCategory(int categId)
        {
            CheckExhibitListClear();

            MySQGetResult($"SELECT * FROM exhibits WHERE categoryid = {categId}");

            return _list;
        }

        public List<Exhibit> GetByHall(int hallId)
        {
            if(_list == null) GetAllExhibits();
            return _list.FindAll(el => el.ExhibitionHallId == hallId);
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
                    _list.Add(new Exhibit()
                    {
                        Id = _sd.SafeGetNumericData(reader, "id"),
                        ExpositionId = _sd.SafeGetNumericData(reader, "expositionid"),
                        CategoryId = _sd.SafeGetNumericData(reader, "categoryid"),
                        ExhibitionHallId = _sd.SafeGetNumericData(reader, "exhibitionhallid"),
                        Title = _sd.SafeGetStringData(reader, "title"),
                        Description = _sd.SafeGetStringData(reader, "description"),
                        Images = _sd.SafeGetStringData(reader, "images").Split('#'),
                        IsTransmitted = _sd.SafeGetNumericData(reader, "istransmitted"),
                        WhereTransmittedId = _sd.SafeGetNumericData(reader, "wheretransmittedid"),
                        InvNum = _sd.SafeGetStringData(reader, "invnum")
                    });
                }
            }
        }

        public List<Exhibit> GetExhibitsByCategory(int categoryId)
        {
            if(_list ==  null) GetAllExhibits();
            return _list.FindAll(element => element.CategoryId == categoryId);
        }

        public Exhibit GetExhibitById(int id)
        {
            if (_list == null) GetAllExhibits();
            return _list.Find(element => element.Id == id);
        }

        public void AddExhibit(string name, int catid, int hallid, string description, string invnum, string images)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO exhibits (categoryid, exhibitionhallid, title, description, images, invnum) VALUES ('" + catid +"','" + 
                hallid + "','" + name + "','" + description + "','" + images + "','" + invnum  +"')";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Delete(int id) 
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM exhibits WHERE id=" + id, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Edit(int id, int catid, int hallid, string title, string description, int invnum, string images)
        {
            using MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE exhibits SET categoryid='" + catid + "',exhibitionhallid='" + hallid 
                + "',title='" + title + "',description='" + description + "',images='" + images + "',invnum='" + invnum + "' WHERE id=" + id, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
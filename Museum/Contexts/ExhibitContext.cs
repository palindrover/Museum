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
                        Images = _sd.SafeGetStringData(reader, "images"),
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
    }
}
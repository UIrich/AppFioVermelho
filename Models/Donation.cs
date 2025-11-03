using SQLite;

namespace AppFioVermelho.Models
{
    public class Donation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Instituicao { get; set; }
        public string TipoSangue { get; set; }
        public DateTime Data { get; set; }
        public string UserEmail { get; set; } 
    }
}

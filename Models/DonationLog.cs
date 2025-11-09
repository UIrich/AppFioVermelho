using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppFioVermelho.Models
{
    public class DonationLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int DonationId { get; set; }
        public string Instituicao { get; set; }
        public string TipoSangue { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;


    }
}

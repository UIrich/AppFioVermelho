using SQLite;

namespace AppFioVermelho.Models
{
    public class DonationLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int DonationId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

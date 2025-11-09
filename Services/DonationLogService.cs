using AppFioVermelho.Models;
using SQLite;

namespace AppFioVermelho.Services
{
    public class DonationLogService
    {
        private readonly DatabaseService _databaseService;
        private SQLiteConnection _connection;

        public DonationLogService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _connection = _databaseService.GetConnection();
            _connection.CreateTable<DonationLog>();
        }

        public void Inserir(DonationLog log)
        {
            _connection.Insert(log);
        }

        public void Atualizar(DonationLog log)
        {
            _connection.Update(log);
        }

        public void Deletar(DonationLog log)
        {
            _connection.Delete(log);
        }

        public DonationLog GetLog(int id)
        {
            return _connection.Table<DonationLog>().FirstOrDefault(l => l.Id == id);
        }

        public List<DonationLog> GetLogs()
        {
            return _connection.Table<DonationLog>().ToList();
        }

        public List<DonationLog> GetLogsPorUsuario(int userId)
        {
            return _connection.Table<DonationLog>().Where(l => l.UserId == userId).ToList();
        }

        public List<DonationLog> GetLogsPorDoacao(int donationId)
        {
            return _connection.Table<DonationLog>().Where(l => l.DonationId == donationId).ToList();
        }
    }
}

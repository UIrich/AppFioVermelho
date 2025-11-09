using AppFioVermelho.Models;
using SQLite;

namespace AppFioVermelho.Services
{
    public class DonationService
    {
        private readonly DatabaseService _databaseService;
        private SQLiteConnection _connection;

        public DonationService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _connection = _databaseService.GetConnection();
            _connection.CreateTable<Donation>(); 
        }

        public void Inserir(Donation donation)
        {
            _connection.Insert(donation);
        }

        public void Atualizar(Donation donation)
        {
            _connection.Update(donation);
        }

        public void Deletar(Donation donation)
        {
            _connection.Delete(donation);
        }

        public Donation GetDoacao(int id)
        {
            return _connection.Table<Donation>().FirstOrDefault(d => d.Id == id);
        }

        public List<Donation> GetDoacoes()
        {
            return _connection.Table<Donation>().ToList();
        }

        public List<Donation> GetDoacoesPorUsuario(string userEmail)
        {
            return _connection.Table<Donation>().Where(d => d.UserEmail == userEmail).ToList();
        }
    }
}

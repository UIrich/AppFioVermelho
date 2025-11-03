using AppFioVermelho.Models;
using SQLite;

namespace AppFioVermelho.Services
{
    public class UserService
    {
        private readonly DatabaseService _databaseService;
        private SQLiteConnection _connection;

        public UserService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _connection = _databaseService.GetConnection();
            _connection.CreateTable<User>();
        }

        public void Inserir(User user)
        {
            _connection.Insert(user);
        }

        public User GetUsuario(int id)
        {
            return _connection.Table<User>().FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsuarios()
        {
            return _connection.Table<User>().ToList();
        }

        public User GetUsuarioPorEmail(string email)
        {
            return _connection.Table<User>().FirstOrDefault(u => u.Email == email);
        }
    }
}

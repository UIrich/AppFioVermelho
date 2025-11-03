using SQLite;
using PCLExt.FileStorage.Folders;

namespace AppFioVermelho.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _connection;

        public DatabaseService()
        {
            var folder = new LocalRootFolder();
            var file = folder.CreateFile("fiovermelho.db", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            _connection = new SQLiteConnection(file.Path);
        }

        public SQLiteConnection GetConnection()
        {
            return _connection;
        }
    }
}

using SQLite;
using PCLExt.FileStorage.Folders;

namespace AppFioVermelho.Services
{
    public class DatabaseService
    {
        public SQLiteConnection connection;

        public SQLiteConnection GetConnection()
        {
            var folder = new LocalRootFolder();
            var file = folder.CreateFile("fiovermelho.db", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            return new SQLiteConnection(file.Path);
        }
    }
}

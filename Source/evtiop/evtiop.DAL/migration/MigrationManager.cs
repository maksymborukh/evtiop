using evtiop.DAL.Core;
using System.IO;

namespace evtiop.DAL.Migration
{
    public partial class MigrationManager
    {
        private const double version = 1.0;

        public void StartMigration()
        {
            DBManager dbManager = new DBManager("shopdb");

            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            DirectoryInfo TablesdirectoryInfo = new DirectoryInfo(path + @"\evtiop.DAL\Migration\Scripts\Tables\");

            //string tables = "select count(*) from INFORMATION_SCHEMA.TABLES where table_type = 'BASE TABLE' and table_schema = 'public';";

            foreach (var file in TablesdirectoryInfo.GetFiles("*.sql"))
            {

                string scr = File.ReadAllText(file.FullName);
                dbManager.ExecuteNonQuery(scr);
            }
            dbManager.ExecuteNonQuery($"insert into dbversion (dbv) values ('{version}')");

            string script = File.ReadAllText(path + @"\evtiop.DAL\Migration\Scripts\Tables\dbversion.sql");
            dbManager.ExecuteNonQuery(script);

        }
    }
}

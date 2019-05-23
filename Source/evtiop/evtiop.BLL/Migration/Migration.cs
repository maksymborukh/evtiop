using evtiop.DAL.Migration;

namespace evtiop.BLL.Migration
{
    public class Migration
    {
        public bool Migrate()
        {
            MigrationManager migrationManager = new MigrationManager();
            try
            {
                migrationManager.StartMigration();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

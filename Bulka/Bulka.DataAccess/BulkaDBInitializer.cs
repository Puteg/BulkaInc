using System.Data.Entity;

namespace Bulka.DataAccess
{
    public class BulkaDbInitializer : DropCreateDatabaseAlways<BulkaContext>
    {
        public BulkaDbInitializer()
        {

        }
    }
}
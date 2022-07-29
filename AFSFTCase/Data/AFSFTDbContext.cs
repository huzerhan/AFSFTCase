using AFSFTCase.Models;
using Microsoft.EntityFrameworkCore;

namespace AFSFTCase.Data
{
    public class AFSFTDbContext:DbContext
    {
        public AFSFTDbContext(DbContextOptions<AFSFTDbContext>options):base(options)
        {

        }
        public DbSet<DataModelEntity> DataModelEntities { get; set; }
    }
}

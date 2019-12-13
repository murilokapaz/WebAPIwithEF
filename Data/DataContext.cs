using Microsoft.EntityFrameworkCore;
using WebApiEf.api.Models;

namespace WebApiEf.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Product> Products {get; set;}//essas são as tabelas das entidades criadas que serão criadas no banco
        public DbSet<Category> Categories { get; set; }
    }
}
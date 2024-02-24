using L01_2020GM604_RESTAURANTE.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GM604_RESTAURANTE.dbContext
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options) {
            
        }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<motoristas> motoristas { get; set;}
        public DbSet<pedidos> pedidos { get; set;}
        public DbSet<platos> platos { get; set;}
    }
}

using Microsoft.EntityFrameworkCore;
using PetsApi.Models;

namespace PetsApi.Data;

public class AnimalDb : DbContext
{
    public AnimalDb(DbContextOptions<AnimalDb> options) : base(options)
    {
        
    }

    public DbSet<Animal> Animais { get; set; }
}

using MvcUnitTesting_dotnet8.Models;
namespace Week2Lab12025.Models;

public class DbSeeder
{
    private readonly BookDbContext _ctx;
    private readonly IWebHostEnvironment _hosting;
    private bool disposedValue;

    public DbSeeder(BookDbContext ctx, IWebHostEnvironment hosting)
    {
        _ctx = ctx;
        _hosting = hosting;
    }
    public void Seed()
    {
        _ctx.Database.EnsureCreated();

        if (!_ctx.Books.Any())
        {
            _ctx.Books.AddRange(new List<Book>()
            {
                new Book { Genre="Fiction", Name="Moby Dick", Price=12.50m},
                new Book { Genre="Fiction", Name="War and Peace", Price=17m},
                new Book { Genre="Science Fiction", Name="Escape from the vortex", Price=12.50m},
                new Book { Genre="History", Name="The Battle of the Somme", Price=22m}
            });
            _ctx.SaveChanges();
        }
    }
}

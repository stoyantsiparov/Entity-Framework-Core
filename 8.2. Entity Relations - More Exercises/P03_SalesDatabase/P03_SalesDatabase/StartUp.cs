using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase;

internal class StartUp
{
    public static void Main()
    {
        var context = new SalesContext();
        context.Database.Migrate();
        context.Seed();
    }
}
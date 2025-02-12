using Models;

public class TestData
{
    private static Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext> options;

    public static void populate(AppDbContext db)
    {
        Customer t = new Customer()
        {
            Id = 1,
            Name = "Stuart 475898",
            Orders = [
                new Order() { Id = 1, Amount=4.3M },
                new Order() { Id = 2, Amount=1.1M }
            ]
        };
        db.Customer.Add(t);

        Customer t2 = new Customer()
        {
            Id = 2,
            Name = "Duncan White"
        };
        db.Customer.Add(t2);


        db.SaveChanges();
    }
}

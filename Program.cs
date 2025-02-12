// Program.cs
using Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
EntitySetConfiguration<Customer> customers = modelBuilder.EntitySet<Customer>("Customers");

modelBuilder.EntitySet<Customer>("Customers");

var services = builder.Services;

services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
    .AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("ODataAuthDemo");
                opt.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
            });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.useMigrationsEndPoint(); 

    using (var scope = app.Services.CreateScope())
    {
        var svc = scope.ServiceProvider;

        var context = svc.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        // DbInitializer.Initialize(context);
        TestData.populate(context);
    }
}
else
{
    app.UseExceptionHandler("/error");
    //app.UseHsts();
}

//app.UseHttpLogging();
app.UseRouting();

app.UseODataRouteDebug();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
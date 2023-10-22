using Microsoft.EntityFrameworkCore;
using TutorialEU.Data;
using TutorialEU.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

// Configurar base de datos Sql Server
builder.Services.AddDbContext<TutorialUEContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("TutorialUEDatabase"));
});


// añadir servicios disponibles para ser inyectados
builder.Services.AddScoped<ProductoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

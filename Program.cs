using Assignment3.ApplicationContext;
using Assignment3.HubServer;
using Microsoft.EntityFrameworkCore;

namespace Assignment3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<PostAppContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("PostAppContext")));
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Create Database if not exist
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var ctx = services.GetRequiredService<PostAppContext>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                DbInit.AppPostIntit(ctx);
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<SignalRHub>("/SignalRHub");

            app.Run();
        }
    }
}
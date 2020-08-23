using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepDeck;
using RepDeck.Data;

[assembly: HostingStartup(typeof(RepDeck.Areas.Identity.IdentityHostingStartup))]
namespace RepDeck.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RepDeckContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RepDeckContextConnection")));

            });
        }
    }
}
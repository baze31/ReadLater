using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadLater5.Areas.Identity.Data;
using ReadLater5.Data;

[assembly: HostingStartup(typeof(ReadLater5.Areas.Identity.IdentityHostingStartup))]
namespace ReadLater5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ReadLater5Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ReadLater5ContextConnection")));

                services.AddDefaultIdentity<ReadLater5User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ReadLater5Context>();
            });
        }
    }
}
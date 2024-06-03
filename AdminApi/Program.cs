using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;


namespace YourNamespace
{
    /*
     
       Project info
       
       Project name :       AdminApi
       Project number :     692586858793
       Project ID :         adminapi-419923
       
     */
    public static class Program
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDistributedMemoryCache();             
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "UserSessionCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; 
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
            });      

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                    //options.CallbackPath = "/AdminApi/callback";;                    
                });            
            /*
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Google:ClientId"];;
                    options.ClientSecret = builder.Configuration["Google:ClientSecret"];;
                    options.CallbackPath = "/AdminApi/callback";
                });
            */
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin V1");
                });
            }

            app.UseDeveloperExceptionPage();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();            
            app.UseAuthorization();
            
            app.UseSession();
            
            app.MapControllers();
            app.Run();
        }
    }
}
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SupMagasin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            const string corsURLKEYS = "Security:Cors:Url";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false; // seulement le HTTPS
                    option.SaveToken = true; // On ssave le token
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, // verification du issuer
                        ValidateAudience = true, // verification de l'Audiance 
                        ValidateLifetime = true, // Verification de la durée de vie
                        ValidateIssuerSigningKey = true, // vérification de al clé
                        ValidIssuer = Configuration["jwt:issuer"], // qui est l'emmeteur ?
                        ValidAudience = Configuration["jwt:audiance"], //  qui est le recepteur ?
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])) // quel est la clé ?
                    };
                });

            services.AddCors(option =>
            {
                string url = Configuration[corsURLKEYS];
                option.AddPolicy("AllowSpecificOrigin",
                           builder => builder.WithOrigins(url)
                                              .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "ApiSupMagasin",Version = "v1"});

           });
        }

    
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api SupMagasin v1");
            });
        }
    }
}

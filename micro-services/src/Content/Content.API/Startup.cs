using Content.API.Data;
using Content.API.Data.Interfaces;
using Content.API.Extensions;
using Content.API.RabbitMQ;
using Content.API.Repositories;
using Content.API.Repositories.Interfaces;
using Content.API.Settings;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Content.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            #region Configuration Dependencies

            services.Configure<ContentDatabaseSettings>(Configuration.GetSection(nameof(ContentDatabaseSettings)));
            services.AddSingleton<IContentDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ContentDatabaseSettings>>().Value);

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IContentContext, ContentContext>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleAuditRepository, ArticleAuditRepository>();
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }
                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }
                return new RabbitMQConnection(factory);
            });
            services.AddSingleton<EventBusRabbitMQConsumer>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Content API", Version = "v1" });
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin",
            //        builder => builder.WithOrigins("http://localhost:4200"));
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Initilize Rabbit Listener in ApplicationBuilderExtentions
            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Content API V1");
            });
        }
    }
}

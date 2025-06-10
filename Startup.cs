using DVotingBackendApp.repositories.interfaces;
using DVotingBackendApp.repositories;
using DVotingBackendApp.services.interfaces;
using DVotingBackendApp.services;
using DVotingBackendApp.utils;

namespace TaskManagementSystem
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IVoterRepository, VoterRepository>();
            services.AddScoped<IConstituencyRepository, ConstituencyRepository>();

            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IVoterService, VoterService>();
            services.AddScoped<IConstituencyService, ConstituencyService>();

            services.AddSingleton<BlockchainService>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


}

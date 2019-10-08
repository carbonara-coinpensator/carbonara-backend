using System;
using System.IO;
using System.Reflection;
using Carbonara.Providers;
using Carbonara.Providers.ChartProvider;
using Carbonara.Providers.CountryCo2EmissionProvider;
using Carbonara.Providers.HashRatePerPoolProvider;
using Carbonara.Providers.PoolHashRateProvider;
using Carbonara.Services.BlockParametersService;
using Carbonara.Services.ChartService;
using Carbonara.Services.CalculationService;
using Carbonara.Services.CountryCo2EmissionService;
using Carbonara.Services.HashRatePerPoolService;
using Carbonara.Services.HttpClientHandler;
using Carbonara.Services.MiningHardwareService;
using Carbonara.Services.NetworkHashRateService;
using Carbonara.Services.PoolHashRateService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Carbonara.Services.BitcoinWalletInformationService;
using Carbonara.Providers.BitcoinWalletProvider;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Carbonara
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Carbonara API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();
            });

            services.AddSingleton<IHttpClientHandler, HttpClientHandler>();
            services.AddSingleton<ICloudFlareHttpClientHandler, CloudFlareHttpClientHandler>();
            services.AddScoped<ICalculationService, CalculationService>();
            services.AddScoped<IBlockParametersService, BlockParametersService>();
            services.AddScoped<INetworkHashRateService, NetworkHashRateService>();
            services.AddScoped<IMiningHardwareService, MiningHardwareService>();
            services.AddScoped<IPoolHashRateService, PoolHashRateService>();
            services.AddScoped<ICountryCo2EmissionService, CountryCo2EmissionService>();
            services.AddScoped<IHashRatePerPoolService, HashRatePerPoolService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<IBitcoinWalletInformationService, BitcoinWalletInformationService>();

            services.AddScoped<IBlockExplorerProvider, BlockExplorerProvider>();
            services.AddScoped<IGlobalHashRateProvider, GlobalHashRateProvider>();
            services.AddScoped<IHardwareProvider, HardwareProvider>();
            services.AddScoped<IPoolHashRateProvider, PoolHashRateProvider>();
            services.AddScoped<ICountryCo2EmissionProvider, CountryCo2EmissionProvider>();
            services.AddScoped<IHashRatePerPoolProvider, HashRatePerPoolProvider>();
            services.AddScoped<ICountryCo2EmissionProvider, CountryCo2EmissionProvider>();
            services.AddScoped<IChartProvider, ChartProvider>();
            services.AddScoped<IBitcoinWalletProvider, BitcoinWalletProvider>();

            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                context.Response.StatusCode = ex.Error.GetType() == typeof(ThirdPartyApiUnreachableException) ?
                                    (int)HttpStatusCode.BadGateway :
                                    (int)HttpStatusCode.InternalServerError;
                                context.Response.ContentType = "text/html";
                                await context.Response.WriteAsync(ex.Error.Message).ConfigureAwait(false);
                            });
                    }
                );
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carbonara API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}

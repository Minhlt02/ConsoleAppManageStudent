using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using ProtoBuf.Grpc.Server;
using Server.DBHelper;
using Server.Mapper;
using Server.Repository;
using Server.Service;
using Shared;
using ISession = NHibernate.ISession;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(7050, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                    listenOptions.UseHttps();
                });
            });
            string connectionString = builder.Configuration.GetConnectionString("Default")!;

            // Add services to the container.
            builder.Services.AddSingleton<NHibernateHelper>(provider => new NHibernateHelper());
            builder.Services.AddScoped(provider => provider.GetService<NHibernateHelper>()!.OpenSession());

            // Add automapper profiles
            builder.Services.AddAutoMapper(typeof(StudentMapper));

            // Add repositories
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();

            // Add gRPC services
            builder.Services.AddScoped<IStudentContract, StudentService>();

            builder.Services.AddCodeFirstGrpc();
            builder.Services.AddGrpcReflection();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapGrpcReflectionService();
            }

            // Configure the HTTP request pipeline.
            app.MapGrpcService<StudentService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
using ConsoleClient.Controller;
using ConsoleClient.Mapper;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;

namespace ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped(provider =>
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7050");
                    return channel.CreateGrpcService<IStudentContract>();
                })
                .AddTransient<StudentController>()
                .AddAutoMapper(typeof(StudentMapper))
                .BuildServiceProvider();

            var studentController = serviceProvider.GetService<StudentController>()!;

            await studentController.MenuAsync();
        }
    }
}
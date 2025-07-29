using BlazorClient.Components;
using BlazorClient.Mapper;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string grpcAddress = builder.Configuration.GetConnectionString("gRPCAddress1")!;
var handler = new SocketsHttpHandler
{
    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
    EnableMultipleHttp2Connections = true
};

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddAntDesign();
builder.Services.AddAutoMapper(typeof(StudentMapper));
builder.Services.AddAutoMapper(typeof(ClassroomMapper));

builder.Services.AddScoped(provider =>
{
    var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
    {
        HttpHandler = handler
    });
    return channel.CreateGrpcService<IStudentContract>();
});

builder.Services.AddScoped(provider =>
{
    var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
    {
        HttpHandler = handler
    });
    return channel.CreateGrpcService<IClassroomContract>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

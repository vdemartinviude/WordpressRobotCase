using JsonDocumentsManager;
using MediatR;
using StateExecute;
using System.Reflection;
using TheRobot;
using TheRobot.DriverService;
using TheRobot.PipelineExceptionHandler;
using TheStateMachine;
using TheStateMachine.Helpers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddSingleton<Robot>();
        services.AddSingleton(x => new InputJsonDocument("./JsonDocuments/InputData.json"));
        services.AddSingleton<ResultJsonDocument>();
        services.AddSingleton(x => TheStateMachineHelpers.GetMachineSpecification(Assembly.Load("WordpressStatesAndGuards")));
        services.AddSingleton<TheMachine>();
        services.AddHostedService<Worker>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("TheRobot")));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));
        services.AddSingleton<WebDriverService>();
    })
    .Build();

host.Run();
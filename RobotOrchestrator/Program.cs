using JsonDocumentsManager;
using StateExecute;
using System.Reflection;
using TheRobot;
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
    })
    .Build();

host.Run();
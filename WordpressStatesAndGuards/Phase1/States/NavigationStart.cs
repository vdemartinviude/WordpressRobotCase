using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class NavigationStart : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromSeconds(60);

    public NavigationStart(StateInfrastructure infrastructure) : base("NavigationStart", infrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        _stateInfra.ResultJsonDocument.AddResultMessage("StartTime", $"Robot start working at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        await _stateInfra.Robot.Execute(new MediatedNavigationRequest
        {
            BaseParameters = new() { TimeOut = TimeSpan.FromSeconds(10) },
            Url = _stateInfra.InputJsonDocument.GetStringData("$.Url")
        }, token);
    }
}
using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class NavigateToLogin : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromSeconds(60);

    public NavigateToLogin(StateInfrastructure infra) : base("NavigateToLogin", infra)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        await _stateInfra.Robot.Execute(new MediatedNavigationRequest
        {
            Url = _stateInfra.InputJsonDocument.GetStringData("$.UrlAdmin")
        }, token);
    }
}
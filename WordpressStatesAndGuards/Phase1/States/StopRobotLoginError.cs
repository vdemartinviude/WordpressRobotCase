using StatesAndEvents;

namespace WordpressStatesAndGuards.States;

public class StopRobotLoginError : BaseState
{
    public StopRobotLoginError(StateInfrastructure infrastructure) : base("StopRobotLoginError", infrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        _stateInfra.ResultJsonDocument.AddResultMessage("Error", "The robot can not execute because a login error ocorred");
        _stateInfra.ResultJsonDocument.AddResultMessage("EndTime", $"The robot has ended working at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        await _stateInfra.ResultJsonDocument.SaveDocument("result.json");
    }
}
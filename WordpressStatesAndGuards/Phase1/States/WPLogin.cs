using OpenQA.Selenium;
using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class WPLogin : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromMinutes(10);

    public WPLogin(StateInfrastructure stateInfrastructure) : base("WPLogin", stateInfrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("user_login")) },
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.UserLogin"),
            KindOfSetText = KindOfSetText.SetByWebDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("user_pass")) },
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.Password"),
            KindOfSetText = KindOfSetText.SetByWebDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest { BaseParameters = new() { ByOrElement = new(By.Id("wp-submit")) } }, token);
    }
}
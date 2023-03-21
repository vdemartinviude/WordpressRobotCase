using OpenQA.Selenium;
using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class WPInitialConfiguration : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromMinutes(10);

    public WPInitialConfiguration(StateInfrastructure infrastructure) : base("WPInitialConfiguration", infrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        await _stateInfra.Robot.Execute(new MediatedSelectRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("language")) },
            Text = String.IsNullOrEmpty(_stateInfra.InputJsonDocument.GetStringData("$.Language")) ? "Português do Brasil" : _stateInfra.InputJsonDocument.GetStringData("$.Language"),
            KindOfSelect = KindOfSelect.SelectByDriveByText
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("language-continue")) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("weblog_title")) },
            KindOfSetText = KindOfSetText.SetByWebDriver,
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.Title")
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("user_login")) },
            KindOfSetText = KindOfSetText.SetByWebDriver,
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.UserLogin")
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("pass1")) },
            KindOfSetText = KindOfSetText.SetByWebDriver,
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.Password")
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("admin_email")) },
            KindOfSetText = KindOfSetText.SetByWebDriver,
            TextToSet = _stateInfra.InputJsonDocument.GetStringData("$.Email")
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("submit")) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//a[1]")) }
        }, token);
    }
}
using OpenQA.Selenium;
using StatesAndEvents;
using TheRobot;
using TheRobot.MediatedRequests;
using WordpressStatesAndGuards.States;

namespace WordpressStatesAndGuards.Guards;

public class LoginFailGuard : IGuard<WPLogin, StopRobotLoginError>
{
    public uint Priority => 5;

    public async Task<bool> Condition(Robot robot, CancellationToken token)
    {
        var response = await robot.Execute(new MediatedElementExistsRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//div[@id='login_error']")) }
        }, token);
        return response.Match(_ => false, _ => true);
    }
}
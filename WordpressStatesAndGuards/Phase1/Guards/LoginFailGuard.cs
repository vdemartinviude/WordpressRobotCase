using OpenQA.Selenium;
using StatesAndEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TheRobot;
using TheRobot.MediatedRequests;
using TheRobot.Requests;
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
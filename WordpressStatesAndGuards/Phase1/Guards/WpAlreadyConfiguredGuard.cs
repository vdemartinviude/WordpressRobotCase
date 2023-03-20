using OpenQA.Selenium;
using StatesAndEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot;
using TheRobot.MediatedRequests;
using TheRobot.Requests;
using WordpressStatesAndGuards.States;

namespace WordpressStatesAndGuards.Guards;

public class WpAlreadyConfiguredGuard : IGuard<NavigationStart, NavigateToLogin>
{
    public uint Priority => 10;

    public async Task<bool> Condition(Robot robot, CancellationToken token)
    {
        var result = await robot.Execute(new MediatedElementExistsRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//body")) }
        }, token);

        return result.Match(x => false, x => true);
    }
}
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

public class WpNotConfigGuard : IGuard<NavigationStart, WPInitialConfiguration>
{
    public uint Priority => 5;

    public async Task<bool> Condition(Robot robot, CancellationToken token)
    {
        var languageSelectExit = await robot.Execute(new MediatedElementExistsRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("language")), TimeOut = TimeSpan.FromSeconds(5) }
        }, token);
        return languageSelectExit.IsT1;
    }
}
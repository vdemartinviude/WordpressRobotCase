using OpenQA.Selenium;
using StatesAndEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot;
using TheRobot.Requests;
using WordpressStatesAndGuards.States;

namespace WordpressStatesAndGuards.Guards;

public class WpAlreadyConfiguredGuard : IGuard<NavigationStart, NavigateToLogin>
{
    public uint Priority => 10;

    public bool Condition(Robot robot)
    {
        var resp = robot.Execute(new IsPageLoadedRequest()).Result;
        return resp.Status == TheRobot.Response.RobotResponseStatus.ActionRealizedOk;
    }
}
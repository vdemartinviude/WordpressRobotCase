﻿using StatesAndEvents;
using TheRobot;
using WordpressStatesAndGuards.States;

namespace WordpressStatesAndGuards.Guards;

public class LoginOkGuard : IGuard<WPLogin, VerifyPluginAllInOneInstall>
{
    public uint Priority => 20;

    public async Task<bool> Condition(Robot robot, CancellationToken token)
    {
        return await Task.Run(() => { return true; });
    }
}
using StatesAndEvents;
using TheRobot;
using WordpressStatesAndGuards.States;

namespace WordpressStatesAndGuards.Guards;

public class NavigatedToLoginScreenGuard : IGuard<NavigateToLogin, WPLogin>
{
    public uint Priority => 10;

    public async Task<bool> Condition(Robot robot, CancellationToken token)
    {
        return await Task.Run(() => { return true; });
    }
}
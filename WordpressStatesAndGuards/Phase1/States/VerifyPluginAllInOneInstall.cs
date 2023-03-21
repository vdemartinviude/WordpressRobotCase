using OpenQA.Selenium;
using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class VerifyPluginAllInOneInstall : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromMinutes(60);

    public VerifyPluginAllInOneInstall(StateInfrastructure infrastructure) : base("PluginAllInOneInstall", infrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        var pluginalreadyinstalled = await _stateInfra.Robot.Execute(new MediatedElementExistsRequest()
        {
            BaseParameters = new()
            {
                ByOrElement = new(By.XPath("//div[contains(@class,'wp-menu-name') and contains(text(),'All-in-One WP Migration')]")),
                TimeOut = TimeSpan.FromSeconds(2)
            }
        }, token);

        if (pluginalreadyinstalled.IsT1)
        {
            return;
        }

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//div[contains(text(),'Plugins')]")), DelayAfter = TimeSpan.FromSeconds(3) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//a[contains(text(),'Adicionar novo') and contains(@class,'page-title-action')]")) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedSetTextRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("search-plugins")) },
            TextToSet = "All-in-one WP Migration",
            KindOfSetText = KindOfSetText.SetByWebDriver
        }, token);
        var buttonInstall = await _stateInfra.Robot.Execute(new MediatedWaitElementExistOrVanish
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//a[contains(@class,'install-now button') and contains(@data-name,'All-in-One')]")), TimeOut = TimeSpan.FromSeconds(1800) }
        }, token);

        buttonInstall.AsT1.WebElement.Click();

        var buttonAtivar = await _stateInfra.Robot.Execute(new MediatedWaitElementExistOrVanish
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//a[contains(text(),'Ativar') and contains(@data-name,'All-in-One')]")), TimeOut = TimeSpan.FromSeconds(1800) }
        }, token);

        buttonAtivar.AsT1.WebElement.Click();
    }
}
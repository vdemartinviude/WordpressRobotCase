using OpenQA.Selenium;
using StatesAndEvents;
using TheRobot.MediatedRequests;

namespace WordpressStatesAndGuards.States;

public class LoadWpressFile : BaseState
{
    public override TimeSpan StateTimeout => TimeSpan.FromMinutes(60);

    public LoadWpressFile(StateInfrastructure stateInfrastructure) : base("LoadWpressFile", stateInfrastructure)
    {
    }

    public override async Task Execute(CancellationToken token)
    {
        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//div[contains(@class,'wp-menu-name') and contains(text(),'All-in-One WP Migration')]")) },
            Kind = KindOfClik.ClickByDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//a[contains(text(),'Importar') and contains(@href,'wm_import')]")) },
            Kind = KindOfClik.ClickByDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//div[contains(@class,'button-main')]")) },
            Kind = KindOfClik.ClickByDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedUploadFileBySelectRequest
        {
            BaseParameters = new() { ByOrElement = new(By.Id("ai1wm-select-file")) },
            FilePath = _stateInfra.InputJsonDocument.GetStringData("$.WpressFilePath")
        }, token);

        var resp = await _stateInfra.Robot.Execute(new MediatedWaitElementExistOrVanish
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//button[contains(text(),'Continuar') and @class='ai1wm-button-green']")), TimeOut = TimeSpan.FromSeconds(1800) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new((WebElement)resp.AsT1.WebElement) },
            Kind = KindOfClik.ClickByDriver
        }, token);

        await _stateInfra.Robot.Execute(new MediatedWaitElementExistOrVanish
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//p[contains(text(),'Restaurando')]")), TimeOut = TimeSpan.FromSeconds(1800) }
        }, token);

        var resp2 = await _stateInfra.Robot.Execute(new MediatedWaitElementExistOrVanish
        {
            BaseParameters = new() { ByOrElement = new(By.XPath("//button[contains(text(),'Finalizar')]")), TimeOut = TimeSpan.FromSeconds(1800) }
        }, token);

        await _stateInfra.Robot.Execute(new MediatedClickRequest
        {
            BaseParameters = new() { ByOrElement = new((WebElement)resp2.AsT1.WebElement) },
            Kind = KindOfClik.ClickByDriver
        }, token);
    }
}
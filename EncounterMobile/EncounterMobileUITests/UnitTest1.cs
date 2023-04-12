using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace EncounterMobileUITests;

public class Tests
{
    AppiumDriver driver;

    public static AppiumOptions GetIosOptions(string path)
    {

        var options= new AppiumOptions();
        options.AutomationName = "xcuitest"; //uiautomator2
        options.BrowserName = "";
        options.PlatformName = "iOS";
        options.PlatformVersion = "16.2";
        options.DeviceName = "iPhone 14 Pro";
        options.App = path;
        return options;
    }

    [OneTimeSetUp]
    //[Test]
    public void SetupAll()
    {
        var assemblyLoc = Assembly.GetExecutingAssembly().Location;
        //var uri = new UriBuilder(Assembly.GetExecutingAssembly().Location);
        //var assemblyDir = Uri.UnescapeDataString(uri.Path);
        //var path = Regex.Replace(assemblyLoc, @"(\/[^\/]*){4}$", "/AppiumProofOfConcept/bin/iPhoneSimulator/Debug/AppiumProofOfConcept.app");
        var path = Regex.Replace(assemblyLoc, @"(\/[^\/]*){5}$", "/EncounterMobile/bin/Debug/net7.0-ios/iossimulator-x64/EncounterMobile.app");
        
        var options = GetIosOptions(path);

        Uri serverUri = new Uri("http://127.0.0.1:4723");
        driver = new IOSDriver(serverUri, options);// AppiumDriver<Appium(serverUri, options);//, /*timeout*/TimeSpan.Zero);
        driver.CloseApp();
        //driver.LaunchApp();
        //driver.Manage().Timeouts().ImplicitlyWait(/*timeout*/TimeSpan.Zero);

    }

    [SetUp]
    public void Setup()
    {
        driver.LaunchApp();
    }

    [TearDown]
    public void Teardown()
    {
        driver.CloseApp();
    }

    [Test]
    public void PassTest()
    {
        Assert.Pass();
    }

    [Test]
    public void FailTest()
    {
        Assert.Fail();
    }
}

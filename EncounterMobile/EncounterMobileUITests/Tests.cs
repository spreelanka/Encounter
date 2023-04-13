using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.MultiTouch;

namespace EncounterMobileUITests;

//{
//  "appium:automationName": "xcuitest",
//  "platformName": "iOS",
//  "appium:deviceName": "iPhone 14 Pro"
//}


//{
//  "appium:automationName": "uiautomator2",
//  "platformName": "Android",
//  "appium:deviceName": "Pixel 3 API 32",
//  "platformVersion": "12"
//}

//[TestFixtureSource(typeof(PlatformFixtureData), nameof(PlatformFixtureData.FixtureParams))]
public class Tests
{
    AppiumDriver driver;

    //public Tests(PlatformFixtureData.GetDriverDelegate getDriver)
    //{
    //    this.driver = getDriver();
    //}

    [OneTimeSetUp]
    public void SetupAll()
    {
        driver = PlatformFixtureData.GetAndroidDriver();
       
        //var assemblyLoc = Assembly.GetExecutingAssembly().Location;
        //var path = Regex.Replace(assemblyLoc, @"(\/[^\/]*){5}$", "/EncounterMobile/bin/Debug/net7.0-ios/iossimulator-x64/EncounterMobile.app");

        //var options = GetIosOptions(path);

        //Uri serverUri = new Uri("http://127.0.0.1:4723");
        //driver = new IOSDriver(serverUri, options);
        //driver.CloseApp();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
        driver.Quit();
    }

    [Test]
    public void MapTile_DefeatDoesChangeMonsterImage()
    {
        var monsterXPath = "(//XCUIElementTypeImage[@name=\"EncounterImage\"])[5]";
        var otherXPath = "(//XCUIElementTypeImage[@name=\"EncounterImage\"])[2]";
        var beforeOtherImage = driver.FindElement(By.XPath(otherXPath));
        var beforeOther = beforeOtherImage.GetScreenshot().AsBase64EncodedString;

        var beforeImage = driver.FindElement(By.XPath(monsterXPath));
        var beforeMonster = beforeImage.GetScreenshot().AsBase64EncodedString;

        {
            var action = new TouchAction(driver);
            action.Tap(beforeImage);
            action.Perform();
        }

        {
            var defeatedCheckbox = driver.FindElement(By.XPath("//XCUIElementTypeSwitch[@name=\"DefeatedCheckBox\"]"));
            var action = new TouchAction(driver);
            action.Tap(defeatedCheckbox);
            action.Perform();
        }

        {
            var back = driver.FindElement(By.XPath("//XCUIElementTypeButton[@name=\"Back\"]"));
            var action = new TouchAction(driver);
            action.Tap(back);
            action.Perform();
        }

        var afterImage = driver.FindElement(By.XPath(monsterXPath));
        var afterMonster = afterImage.GetScreenshot().AsBase64EncodedString;

        var afterOtherEle = driver.FindElement(By.XPath(otherXPath));
        var afterOther = afterOtherEle.GetScreenshot().AsBase64EncodedString;

        //monster image on target maptile does change when defeated
        Assert.AreNotEqual(beforeMonster, afterMonster);

        //monster image on non-target maptile does not change
        Assert.AreEqual(beforeOther, afterOther);
    }

    //[Test]
    //public void PassTest()
    //{
    //    Assert.Pass();
    //}

    //[Test]
    //public void FailTest()
    //{
    //    Assert.Fail();
    //}
}

public class PlatformFixtureData
{
    public static AppiumOptions GetIosOptions(string path)
    {
        var options = new AppiumOptions();
        options.AutomationName = "xcuitest"; //uiautomator2
        options.BrowserName = "";
        options.PlatformName = "iOS";
        options.PlatformVersion = "16.2";
        options.DeviceName = "iPhone 14 Pro";
        options.App = path;
        return options;
    }

    public static AppiumOptions GetAndroidOptions(string path)
    {
        var options = new AppiumOptions();
        options.AutomationName = "uiautomator2";
        options.BrowserName = "";
        options.PlatformName = "Android";
        options.PlatformVersion = "12";//"emulator-5554 (12)";
        options.DeviceName = "Pixel 3 API 32"; // (API 31)
        //options.AddAdditionalAppiumOption("appPackage", "com.companyname.encountermobile");
        //options.AddAdditionalAppiumOption("appActivity", "com.companyname.encountermobile.MainActivity");
        //options.AddAdditionalAppiumOption("appPackage", "com.companyname.encountermobile");
        //options.AddAdditionalAppiumOption("appActivity", "com.companyname.encountermobile / crc640045194c35012d98.MainActivity");
        
        //options.App = path;
        return options;
    }

    public static AppiumDriver GetIosDriver()
    {
        var assemblyLoc = Assembly.GetExecutingAssembly().Location;
        var path = Regex.Replace(assemblyLoc, @"(\/[^\/]*){5}$", "/EncounterMobile/bin/Debug/net7.0-ios/iossimulator-x64/EncounterMobile.app");

        var options = GetIosOptions(path);

        Uri serverUri = new Uri("http://127.0.0.1:4723");
        var driver = new IOSDriver(serverUri, options);
        return driver;
    }

    public static AppiumDriver GetAndroidDriver()
    {
        var assemblyLoc = Assembly.GetExecutingAssembly().Location;
        var path = Regex.Replace(assemblyLoc, @"(\/[^\/]*){5}$", "/EncounterMobile/bin/Debug/net7.0-android/com.companyname.encountermobile-Signed.apk");

        var options = GetAndroidOptions(path);

        Uri serverUri = new Uri("http://127.0.0.1:4723");
        var driver = new AndroidDriver(serverUri, options);
        driver.ActivateApp("com.companyname.encountermobile");
        return driver;
    }
    public delegate AppiumDriver GetDriverDelegate();

    public static IEnumerable<TestFixtureData> FixtureParams
    {
        get
        {
            //yield return new TestFixtureData(new GetDriverDelegate(GetAndroidDriver));
            yield return new TestFixtureData(new GetDriverDelegate(GetIosDriver));
        }
    }
}

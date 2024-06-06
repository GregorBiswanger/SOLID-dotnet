using System;
using Xunit;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace SOLID_dotnet.Tests;

public class PlzSuperToolE2ETests : IDisposable
{
    private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
    private const string WpfAppId = @"C:\projects\workshops\dotnet-refactoring\SOLID-dotnet\PlzSuperTool\bin\Debug\net6.0-windows\PlzSuperTool.exe";

    private WindowsDriver<WindowsElement> _session;

    public PlzSuperToolE2ETests()
    {
        if (_session == null)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", WpfAppId);
            _session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
            _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }
    }

    public void Dispose()
    {
        if (_session != null)
        {
            _session.Quit();
            _session = null;
        }
    }

    [Fact]
    public void TestPLZSearch()
    {
        // Finde die Textbox und schreibe die Stadt Ingolstadt
        var searchTextBox = _session.FindElementByAccessibilityId("CityNameTextBox");
        searchTextBox.SendKeys("Ingolstadt");

        // Finde den Button und klicke darauf
        var searchButton = _session.FindElementByAccessibilityId("SearchZipsButton");
        searchButton.Click();
        
        // Finde die ListView und überprüfe, ob die PLZ 85055 enthalten ist
        var listBox = _session.FindElementByAccessibilityId("ZipsResultListBox");
        var listItems = listBox.FindElementsByXPath("//*");
        bool plzFound = false;
        foreach (var item in listItems)
        {
            if (item.Text.Contains("85055"))
            {
                plzFound = true;
                break;
            }
        }

        Assert.True(plzFound, "Die PLZ 85055 wurde nicht in der ListBox gefunden.");
    }
}
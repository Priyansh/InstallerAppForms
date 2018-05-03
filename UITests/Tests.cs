using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android
            .ApkFile(@"E:\pri\InstallerAppForms\InstallerAppForms.Android\bin\Release\com.companyname.InstallerAppForms.apk")
            .DeviceSerial("84B7N15A10008165")
            .PreferIdeSettings()
            .EnableLocalScreenshots()
            .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void Login_Empty_UName_And_Pwd_Fails()
        {
            //Arrange
            app.EnterText("auto_txtUserName", "");
            app.EnterText("auto_txtPassword", "");

            //Act
            app.Tap("auto_btnLogin");

            //Assert
            var appResult = app.Query("auto_lblErrorMsg").First(result => result.Text == "Invalid UserName/Password");
            Assert.IsTrue(appResult != null, "Label should display proper message");
        }
    }
}


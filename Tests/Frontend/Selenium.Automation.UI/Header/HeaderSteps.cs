﻿using Selenium.Automation.Model.Domain.Header;
using Selenium.Automation.Model.Platform.Drivers;
using Selenium.Automation.Platform.Factory;

namespace Selenium.Automation.UI.Header
{
    public class HeaderSteps : IHeaderSteps
    {
        private readonly IWebDriver _webDriver;

        public HeaderSteps(
            IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private HeaderPage HeaderPage => PageFactory.Get<HeaderPage>(_webDriver);

        public bool IsLanguageUA() =>
            HeaderPage.LanguageLink.GetText().Equals("RU");

        public void ChangeLanguageUA()
        {
            if (!IsLanguageUA())
                HeaderPage.LanguageLink.Click();
        }
    }
}
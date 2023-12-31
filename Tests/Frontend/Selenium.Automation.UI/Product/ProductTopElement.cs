﻿using OpenQA.Selenium.Support.PageObjects;

using Selenium.Automation.Model.Platform.Locator;
using Selenium.Automation.Platform.Element;
using Selenium.Automation.Platform.WebElements;

namespace Selenium.Automation.UI.Product
{
	public class ProductTopElement : HtmlElement
	{
		[FindBy(How.XPath, ".//h1")]
		public HtmlLabel ProductHeaderLabel { get; set; }
	}
}

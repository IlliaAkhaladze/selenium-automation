﻿using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium.Support.PageObjects;

using Selenium.Automation.Model.Platform.Locator;
using Selenium.Automation.Model.Platform.WebElements;
using Selenium.Automation.Platform.Element;
using Selenium.Automation.Platform.Waiter;

namespace Selenium.Automation.Platform.WebElements
{
	public class SearchDropdown : HtmlElement, ISearchDropdown
	{
		[FindBy(How.XPath, ".//input[@automation-id='sign-up-prefix-input']")]
		private HtmlTextBox HtmlTextBox { get; set; }

		[FindBy(How.XPath, ".//div[@automation-id='sign-up-prefix-item']")]
		private HtmlElement Dropdown { get; set; }

		private IEnumerable<HtmlLink> Items =>
			Dropdown
				.FindAll<HtmlLink>(
					new Locator(
						How.XPath,
						".//a[@class='item']")).ToArray();

		public void Search(string value)
		{
			SetValue(value);
			if (!IsDropdownDisplayed())
			{
				WaitFor.Condition(
					() =>
						Dropdown.GetDisplayed(),
					"The search drop down was not displayed");
			}

			Select(value);
		}

		public void SetValue(string value)
		{
			HtmlTextBox.SetText(value);
			Select(value);
		}

		public void Select(string value) =>
			Dropdown.Find<HtmlLink>(
					new Locator(How.XPath, $".//a[@name='{value.ToLower()}']"))
				.Click();

		public string[] GetValues() =>
			Items.Select(i => i.GetText().Trim()).ToArray();

		public bool IsDropdownDisplayed() => Dropdown.GetDisplayed();
	}
}

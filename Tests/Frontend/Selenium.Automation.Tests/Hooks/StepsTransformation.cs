﻿using System.Linq;

using TechTalk.SpecFlow;

namespace Selenium.Automation.Tests.Hooks
{
	[Binding]
	public class StepsTransformation
	{
		[StepArgumentTransformation]
		public string[] TransformToListOfString(string commaSeparatedList) =>
			commaSeparatedList.Split(";")
				.Select(e => e.Trim()).ToArray();

		[StepArgumentTransformation]
		public bool StringToBool(string value) => Bool.Parse(value);
	}
}

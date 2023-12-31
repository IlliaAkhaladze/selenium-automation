﻿using System;

using Autofac;

using Automation.Common.Environment;

using Microsoft.Extensions.Configuration;

using Selenium.Automation.Domain.Login;
using Selenium.Automation.Domain.Search;
using Selenium.Automation.Model.Domain.Filter;
using Selenium.Automation.Model.Domain.HamburgerMenu;
using Selenium.Automation.Model.Domain.Header;
using Selenium.Automation.Model.Domain.Login;
using Selenium.Automation.Model.Domain.Navigation;
using Selenium.Automation.Model.Domain.Poduct;
using Selenium.Automation.Model.Domain.Search;
using Selenium.Automation.Model.Domain.Slider;
using Selenium.Automation.Model.Platform.Drivers;
using Selenium.Automation.Platform.Configuration.Run;
using Selenium.Automation.Platform.Driver;
using Selenium.Automation.UI.Filter;
using Selenium.Automation.UI.Header;
using Selenium.Automation.UI.Login;
using Selenium.Automation.UI.Menu;
using Selenium.Automation.UI.Navigation;
using Selenium.Automation.UI.Product;
using Selenium.Automation.UI.Search;
using Selenium.Automation.UI.Slider;

using Serilog;
using Serilog.Events;

namespace Selenium.Automation.Tests.Bootstrap
{
	public class Bootstraper
	{
		private ContainerBuilder _builder;

		public ContainerBuilder Builder => _builder ??= new ContainerBuilder();

		public void ConfigureServices(IConfigurationBuilder configurationBuilder)
		{
			var configurationRoot = configurationBuilder.Build();
			Builder.Register<ILogger>((c, p) => new LoggerConfiguration()
				.WriteTo.File(
					$"Logs/log_{DateTime.UtcNow:yyyy_MM_dd_hh_mm_ss}.txt",
					LogEventLevel.Verbose,
					"{Timestamp:dd-MM-yyyy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
				.CreateLogger())
				.SingleInstance();

			// Configurations
			Builder.Register<IEnvironmentConfiguration>(context => configurationRoot.Get<EnvironmentConfiguration>())
				.SingleInstance();
			Builder.Register<IRunSettings>(cont => configurationRoot.Get<RunSettings>())
				.SingleInstance();

			// Logic
			Builder.RegisterType<LoginContext>().As<ILoginContext>().SingleInstance();
			Builder.RegisterType<LoginSteps>().As<ILoginSteps>().SingleInstance();
			Builder.RegisterType<SearchSteps>().As<ISearchSteps>().SingleInstance();
			Builder.RegisterType<ProductTopSteps>().As<IProductTopSteps>().SingleInstance();
			Builder.RegisterType<SearchContext>().As<ISearchContext>().SingleInstance();
			Builder.RegisterType<NavigationSteps>().As<INavigationSteps>().SingleInstance();
			Builder.RegisterType<HamburgerMenuSteps>().As<IHamburgerMenuSteps>().SingleInstance();
			Builder.RegisterType<FilterSteps>().As<IFilterSteps>().SingleInstance();
			Builder.RegisterType<WebDriver>().As<IWebDriver>().SingleInstance();
			Builder.RegisterType<SliderSteps>().As<ISliderSteps>().SingleInstance();
            Builder.RegisterType<CityInHamburgerMenuSteps>().As<ICityInHamburgerMenuSteps>().SingleInstance();
            Builder.RegisterType<HeaderSteps>().As<IHeaderSteps>().SingleInstance();

        }
	}
}

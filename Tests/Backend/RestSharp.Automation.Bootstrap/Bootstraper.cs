﻿using System;

using Autofac;

using Automation.Common.Environment;

using Microsoft.Extensions.Configuration;

using RestSharp.Automation.Model.Platform.Communication;
using RestSharp.Automation.Platform.Communication;

using Serilog;
using Serilog.Events;
using RestSharp.Automation.Domain.PetStore;
using RestSharp.Automation.Model.Domain.PetStore;
using RestSharp.Automation.Domain.Pet;
using RestSharp.Automation.Model.Domain.Pet;
using RestSharp.Automation.Domain.PetStoreUser;
using RestSharp.Automation.Model.Domain.PetStoreUser;

namespace RestSharp.Automation.Bootstrap
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
			Builder.Register<IEnvironmentConfiguration>(context => 
				configurationRoot.Get<EnvironmentConfiguration>())
				.SingleInstance();

			// Api Clients
			Builder.RegisterType<PetStoreApiClient>().As<IPetStoreApiClient>().SingleInstance();
			Builder.RegisterType<Client>().As<IClient>().InstancePerDependency();
			Builder.RegisterType<RestClient>().As<IRestClient>().InstancePerDependency();
            Builder.RegisterType<UserApiClient>().As<IUserApiClient>().SingleInstance();
			Builder.RegisterType<PetStoreApiClient>().As<IPetStoreApiClient>().SingleInstance();
            Builder.RegisterType<PetApiClient>().As<IPetApiClient>().SingleInstance();

            // Logic Steps
            Builder.RegisterType<PetStoreSteps>().As<IPetStoreSteps>().InstancePerDependency();
            Builder.RegisterType<UserSteps>().As<IUserSteps>().InstancePerDependency();
            Builder.RegisterType<PetSteps>().As<IPetSteps>().InstancePerDependency();

            // Logic Context
        }
	}
}

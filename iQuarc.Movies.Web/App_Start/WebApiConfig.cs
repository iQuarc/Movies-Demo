using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Serialization;

namespace iQuarc.Movies.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.DependencyResolver = new ServiceLocatorDependencyResolver(BootstrapConfig.ServiceLocator);

			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			XmlMediaTypeFormatter formatter = config.Formatters.XmlFormatter;
			if (formatter != null)
				config.Formatters.Remove(formatter);

			JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.MaxDepth = 3;
		}
	}

	internal class ServiceLocatorDependencyResolver : IDependencyResolver
	{
		private readonly HashSet<Type> notFound = new HashSet<Type>();
 
		private readonly IServiceLocator serviceLocator;

		public ServiceLocatorDependencyResolver(IServiceLocator serviceLocator)
		{
			this.serviceLocator = serviceLocator;
		}

		public void Dispose()
		{
		}

		public object GetService(Type serviceType)
		{
			if (notFound.Contains(serviceType))
				return null;

			try
			{
				object instance = serviceLocator.GetInstance(serviceType);
				return instance;
			}
			catch (InvalidOperationException)
			{
				notFound.Add(serviceType);
			}
			catch (ActivationException)
			{
				notFound.Add(serviceType);
			}

			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return serviceLocator.GetAllInstances(serviceType);
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}
	}
}

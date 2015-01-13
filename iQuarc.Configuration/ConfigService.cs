using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using iQuarc.AppBoot;

namespace iQuarc.Configuration
{
	[Service(typeof(IConfigService), Lifetime.Application)]
	public class ConfigService : IConfigService
	{
		private readonly Dictionary<string, object> configuration = new Dictionary<string, object>();

		public T GetConfig<T>(string key, T defaultValue = default(T))
		{
			if (key == null) throw new ArgumentNullException("key");

			object cached;
			if (configuration.TryGetValue(key, out cached))
				return cached == null ? defaultValue : (T)cached;

			if (!typeof (T).IsValueType && !typeof (T).IsPrimitive && typeof (T) != typeof (string)) 
				return defaultValue;

			object converted = GetConfig(key, typeof (T));
			configuration[key] = converted;

			if (converted != null)
				return (T) converted;

			return defaultValue;
		}

		public T GetConfig<T>()
		{
			string key = typeof (T).FullName;

			object cached;
			if (configuration.TryGetValue(key, out cached))
				return cached == null ? default(T) : (T)cached;

			object instance = GetConfig(typeof (T));
			configuration[key] = instance;

			return (T) instance;
		}

		private static object GetConfig(Type type)
		{
			object instance = Activator.CreateInstance(type);

			PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo property in properties)
			{
				string propertyKey = type.Name + "." + property.Name;
				object propValue = GetConfig(propertyKey, property.PropertyType);

				if (propValue != null)
					property.SetValue(instance, propValue);
			}

			return instance;
		}

		private static object GetConfig(string key, Type type)
		{
			string value = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrEmpty(value))
				return null;

			object converted = Convert.ChangeType(value, type);

			return converted;
		}
	}
}
namespace iQuarc.Configuration
{
    public interface IConfigService
    {
		T GetConfig<T>(string key, T defaultValue = default (T));
		T GetConfig<T>();
    }
}

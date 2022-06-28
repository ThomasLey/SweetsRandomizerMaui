using AppClient.DataStore;

namespace AppClient.Provider
{
    public interface IDeviceProvider
    {
        ModuleInfo[] GetModules();
    }
}
using AppClient.DataStore;

namespace AppClient.Provider
{
    public interface IModuleProvider
    {
        ModuleInfo[] GetModules();
    }
}
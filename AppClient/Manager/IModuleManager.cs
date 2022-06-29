using AppClient.DataStore;

namespace AppClient.Manager
{
    public interface IModuleManager
    {
        ModuleInfo[] LoadModules();
        void SaveModules(ICollection<ModuleInfo> modules);
    }
}
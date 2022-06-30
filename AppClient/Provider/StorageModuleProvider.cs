using AppClient.DataStore;
using System.Text;
using System.Text.Json;

namespace AppClient.Provider
{
    public class StorageModuleProvider : IModuleProvider
    {
        public ModuleInfo[] LoadModules()
        {
            string modulesBase64 = Preferences.Get("modules", null);
            if (modulesBase64 == null) return Array.Empty<ModuleInfo>();

            string modulesJson = Encoding.ASCII.GetString(Convert.FromBase64String(modulesBase64));
            ModuleInfo[] modules = JsonSerializer.Deserialize<ModuleInfo[]>(modulesJson);
            return modules == null ? Array.Empty<ModuleInfo>() : modules;
        }
    }
}

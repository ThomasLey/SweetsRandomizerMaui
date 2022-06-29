using AppClient.DataStore;
using System.Text;
using System.Text.Json;

namespace AppClient.Manager
{
    public class StorageModuleManager : IModuleManager
    {
        public ModuleInfo[] LoadModules()
        {
            string modulesBase64 = Preferences.Get("modules", null);
            if (modulesBase64 == null) return Array.Empty<ModuleInfo>();

            string modulesJson = Encoding.ASCII.GetString(Convert.FromBase64String(modulesBase64));
            ModuleInfo[] modules = JsonSerializer.Deserialize<ModuleInfo[]>(modulesJson);
            return modules == null ? Array.Empty<ModuleInfo>() : modules;
        }

        public void SaveModules(ICollection<ModuleInfo> modules)
        {
            string modulesJson = JsonSerializer.Serialize(modules);
            string modulesBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(modulesJson));
            Preferences.Set("modules", modulesBase64);
        }
    }
}

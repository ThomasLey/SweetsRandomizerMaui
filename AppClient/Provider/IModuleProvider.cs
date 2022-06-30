using AppClient.DataStore;
using System.Text;
using System.Text.Json;

namespace AppClient.Provider
{
    public interface IModuleProvider
    {
        ModuleInfo[] LoadModules();

        public void SaveModules(ICollection<ModuleInfo> modules)
        {
            string modulesJson = JsonSerializer.Serialize(modules);
            string modulesBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(modulesJson));
            Preferences.Set("modules", modulesBase64);
        }
    }
}
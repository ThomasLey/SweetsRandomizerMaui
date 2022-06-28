using AppClient.Provider;

namespace AppClient.DataStore
{
    public static class ModuleStore
    {

        public static List<ModuleInfo> Modules { get; } = new List<ModuleInfo>();

        static ModuleStore()
        {
            // TODO: Change Provider
            IDeviceProvider provider = new MockDeviceProvider();
            ModuleInfo[] devices = provider.GetModules();
            LoadDevices(devices);
        }

        public static void LoadDevices(ModuleInfo[] devices)
        {
            Modules.Clear();
            Modules.AddRange(devices);
        }

        public static void RegisterDevice(ModuleInfo device)
        {
            Modules.Add(device);
        }

        public static void UnregisterDevice(ModuleInfo device)
        {
            Modules.Remove(device);
        }

    }
}

using AppClient.Provider;

namespace AppClient.DataStore
{
    public static class ModuleStore
    {

        public static readonly List<ModuleInfo> Modules = new List<ModuleInfo>();

        private static readonly IModuleProvider ModuleManager;

        static ModuleStore()
        {
            IModuleProvider moduleManager = new StorageModuleProvider();
            ModuleInfo[] modules = moduleManager.LoadModules();

            if(modules.Length == 0)
            {
#if DEBUG
                moduleManager = new MockModuleProvider();
#else
                moduleManager = new CandyWheelModuleProvider();
#endif

                modules = moduleManager.LoadModules();
            }

            ModuleManager = moduleManager;
            LoadModules(modules);
        }

        public static void LoadModules(ModuleInfo[] modules)
        {
            Modules.Clear();
            Task.WaitAll(CheckConnectionStatusAsync(modules));
            Modules.AddRange(modules);

            ModuleManager.SaveModules(Modules);
        }

        public static void RegisterModule(ModuleInfo module)
        {
            Task.WaitAll(CheckConnectionStatusAsync(module));
            Modules.Add(module);

            ModuleManager.SaveModules(Modules);
        }

        public static void UnregisterModule(ModuleInfo module)
        {
            Modules.Remove(module);

            ModuleManager.SaveModules(Modules);
        }

        public static async Task CheckConnectionStatusAsync(params ModuleInfo[] modules)
        {
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            foreach (ModuleInfo module in modules)
            {
                try
                {
                    using HttpResponseMessage message = await client.GetAsync(module.Host).ConfigureAwait(false);
                    string response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!message.IsSuccessStatusCode)
                        throw new Exception(response);
                    else
                    {
                        module.ConnectionMessage = response;

                        switch (module.Type)
                        { // Connection check
                            case ModuleType.Webpage:
                                module.ConnectionStatus = response.StartsWith("<html>") ?
                                    ConnectionStatus.Online : ConnectionStatus.CheckConnection;
                                break;
                            case ModuleType.SegmentedLights:
                            case ModuleType.SpinningLights:
                            case ModuleType.AnimationLights:
                                module.ConnectionStatus = response.StartsWith("{") ?
                                    ConnectionStatus.Online : ConnectionStatus.CheckConnection;
                                break;

                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
                catch (Exception ex)
                {
                    module.ConnectionMessage = string.IsNullOrWhiteSpace(ex.Message) ?
                        "No Message!" : ex.Message;
                    module.ConnectionStatus = ConnectionStatus.Offline;
                }
            }
        }

    }
}

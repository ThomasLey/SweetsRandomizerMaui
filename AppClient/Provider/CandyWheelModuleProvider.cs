using AppClient.DataStore;

namespace AppClient.Provider
{
    public class CandyWheelModuleProvider : IModuleProvider
    {
        public ModuleInfo[] LoadModules()
        {
            return new ModuleInfo[]
            {
                new ModuleInfo()
                {
                    Name = "Router",
                    Host = "http://192.168.1.1",
                    Type = ModuleType.Webpage,
                    Description = "Der Router"
                },
                new ModuleInfo()
                {
                    Name = "RaspberryPI",
                    Host = "http://192.168.1.33",
                    Type = ModuleType.Webpage,
                    Description = "Der PI"
                },
                new ModuleInfo()
                {
                    Name = "Schrank",
                    Host = "http://192.168.1.2",
                    Type = ModuleType.SegmentedLights,
                    Description = "Der Schrank"
                },
                new ModuleInfo()
                {
                    Name = "Leinwand",
                    Host = "http://192.168.1.3",
                    Type = ModuleType.SpinningLights,
                    Description = "Die Leinwand"
                }
            };
        }
    }
}

using AppClient.DataStore;

namespace AppClient.Provider
{
    public class MockModuleProvider : IModuleProvider
    {
        public ModuleInfo[] LoadModules()
        {
            return new ModuleInfo[]
            {
                new ModuleInfo()
                {
                    Name = "Router",
                    Host = "http://10.0.2.2:8080/router",
                    Type = ModuleType.Webpage,
                    Description = "Der Router"
                },
                new ModuleInfo()
                {
                    Name = "RaspberryPI",
                    Host = "http://10.0.2.2:8080/pi",
                    Type = ModuleType.Webpage,
                    Description = "Der PI"
                },
                new ModuleInfo()
                {
                    Name = "Schrank",
                    Host = "http://10.0.2.2:8080/schrank",
                    Type = ModuleType.SegmentedLights,
                    Description = "Der Schrank"
                },
                new ModuleInfo()
                {
                    Name = "Leinwand",
                    Host = "http://10.0.2.2:8080/leinwand",
                    Type = ModuleType.SpinningLights,
                    Description = "Die Leinwand"
                },
                new ModuleInfo()
                {
                    Name = "Lollipop",
                    Host = "http://10.0.2.2:8080/lollipop",
                    Type = ModuleType.SegmentedLights,
                    Description = "Die Lollipops"
                }
            };
        }
    }
}
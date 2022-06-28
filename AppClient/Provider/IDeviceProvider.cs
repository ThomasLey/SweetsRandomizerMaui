namespace AppClient.Provider
{
    internal interface IDeviceProvider
    {
        Model.Device[] GetDevices();
    }
}
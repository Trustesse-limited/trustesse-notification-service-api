using Ivoluntia.BackgroudServices.Common.Configs;

namespace Ivoluntia.BackgroudServices.Common.Helpers
{
    public static class NotificationSettingsFactory
    {
        public static object DeserializeProviderSettings(string settings, string provider)
        {
            return provider switch
            {
                "Gmail" => JsonConvert.DeserializeObject<GmailClientConfig>(settings),
                _ => throw new NotSupportedException($"Provider '{provider}' is not available.")
            };
        }
    }
}

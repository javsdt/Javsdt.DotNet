namespace Javsdt.Infrastructure.Configurations
{
    internal class InfrastructureSettings
    {
        public ConnectionStringsSettings ConnectionStrings { get; set; } = default!;

        public ThirdPartysSettings ThirdPartys { get; set; } = default!;
    }

    public class ConnectionStringsSettings() {
        public string AppDb { get; set; } = default!;
    }

    public class ThirdPartysSettings()
    {
        public AvpiSettings Avpi { get; set; } = default!;
    }

    public class AvpiSettings()
    {
        public string BaseUrl { get; set; } = default!;
    }
}

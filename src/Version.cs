namespace Vanslate
{
    public static class Version
    {
        public enum DevelopmentStage
        {
            Development,
            Release
        }
        
        public static bool IsDevelopment => ReleaseType is DevelopmentStage.Development;

        public static DevelopmentStage ReleaseType
#if DEBUG
            => DevelopmentStage.Development;
#else
            => DevelopmentStage.Release;
#endif
        public static string FullVersion => $"{Major}.{Minor}.{Patch}-{ReleaseType}";
        public static string DiscordNetVersion => Discord.DiscordConfig.Version;
        public static System.Version AsDotNetVersion() => new(Major, Minor, Patch);
        
        private static int Major => 1;
        private static int Minor => 0;
        private static int Patch => 0;
    }
}

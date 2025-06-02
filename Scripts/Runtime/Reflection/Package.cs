namespace Serein.Reflection
{
    public static class Package
    {
        public const string Name = "Serein";
        public const string TechnicalName = "com.coldwind.serein";
        public const string Path = "Packages/" + TechnicalName;

        public static class ModuleName
        {
            public const string Reflection = nameof(Reflection);
            public const string Debug = nameof(Debug);
            public const string Misc = nameof(Misc);
        }
    }
}

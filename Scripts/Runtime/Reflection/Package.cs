namespace Serein.Reflection
{
    public static class Package
    {
        public const string Name = "Serein";
        public const string TechnicalName = "com.coldwind.serein";
        public const string SystemAddressablesGroupName = "System";
        public const string SystemObjectName = "System";

        public const string Path = "Packages/" + TechnicalName;

        public const string PathToDynamicDataFolder = 
            Metadata.AssetRootFolderName + "/" + Name + "Data";

        public const string PathToMainScene = 
            Metadata.AssetRootFolderName + "/Source/Scenes/Main" + Metadata.UnityExtension;

        public static class ModuleName
        {
            public const string Infrastructure = nameof(Infrastructure);
            public const string Reflection = nameof(Reflection);
            public const string Debug = nameof(Debug);
            public const string Misc = nameof(Misc);

            public const string Deployment = nameof(Deployment);
            public const string Disk = nameof(Disk);
            public const string GUI = nameof(GUI);
        }
    }
}

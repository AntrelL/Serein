using System.Reflection;

namespace Serein.Reflection
{
    public class Metadata
    {
        public const string AssetRootFolderName = "Assets";

        public const string GitKeepFileName = ".gitkeep";
        public const string UnityExtension = ".unity";
        public const string AssetExtension = ".asset";

        public const string EntryPointMethodName = "Main";
        public const BindingFlags EntryPointMethodBindingFlags = 
            BindingFlags.NonPublic | BindingFlags.Static;
    }
}

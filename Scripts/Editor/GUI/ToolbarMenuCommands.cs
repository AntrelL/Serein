using Serein.Deployment.Editor;
using Serein.Disk.Editor;
using Serein.Reflection;
using UnityEditor;

namespace Serein.GUI.Editor
{
    internal class ToolbarMenuCommands
    {
        private const string DeploymentMenuPath =
            Package.Name + "/" + Package.ModuleName.Deployment + "/";

        private static Installation s_installation = new();
        private static FolderArchitecture s_folderArchitecture = new();

        [MenuItem(DeploymentMenuPath + nameof(Install), priority = 1)]
        private static void Install()
        {
            s_installation.Start();
        }

        [MenuItem(DeploymentMenuPath + nameof(DeleteGitkeepFiles), priority = 2)]
        private static void DeleteGitkeepFiles()
        {
            s_folderArchitecture.DeleteGitkeepFiles(Metadata.AssetRootFolderName);
        }
    }
}

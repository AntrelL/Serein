using Serein.Deployment.Editor;
using Serein.Reflection;
using UnityEditor;

namespace Serein.GUI.Editor
{
    internal class ToolbarMenuCommands
    {
        private const string DeploymentMenuPath =
            Package.Name + "/" + Package.ModuleName.Deployment + "/";

        private static FolderArchitecture s_folderArchitecture = new();
        private static Installation s_installation = new();

        [MenuItem(DeploymentMenuPath + nameof(Install))]
        private static void Install()
        {
            s_installation.Start();
        }

        [MenuItem(DeploymentMenuPath + nameof(DeleteGitkeepFiles))]
        private static void DeleteGitkeepFiles()
        {
            s_folderArchitecture.DeleteGitkeepFiles(Metadata.AssetRootFolderName);
        }
    }
}

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

        private const string InstallButtonMenuPath = 
            DeploymentMenuPath + nameof(Install);

        private const string DeleteGitkeepButtonMenuPath =
            DeploymentMenuPath + nameof(DeleteGitkeepFiles);

        private const int InstallButtonPriority = 1;
        private const int DeleteGitkeepButtonPriority = InstallButtonPriority + 1;

        private static Installation s_installation = Installation.Instance;
        private static FolderArchitecture s_folderArchitecture = new();

        [MenuItem(InstallButtonMenuPath, priority = InstallButtonPriority)]
        private static void Install()
        {
            s_installation.Start();
        }

        [MenuItem(InstallButtonMenuPath, true)]
        private static bool ValidateInstall()
        {
            return s_installation.IsCompleted == false;
        }

        [MenuItem(DeleteGitkeepButtonMenuPath, priority = DeleteGitkeepButtonPriority)]
        private static void DeleteGitkeepFiles()
        {
            s_folderArchitecture.DeleteGitkeepFiles(Metadata.AssetRootFolderName);
        }
    }
}

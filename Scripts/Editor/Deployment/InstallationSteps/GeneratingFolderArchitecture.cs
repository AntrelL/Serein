using Serein.Disk.Editor;
using Serein.Reflection;
using System.Collections.Generic;

namespace Serein.Deployment.Editor
{
    internal class GeneratingFolderArchitecture : InstallationStep
    {
        private const string PathToFolderWithStructures =
            Package.Path + "/Data/Editor/FolderStructures/";

        private readonly List<string> _folderStructureNames = new()
        {
            "DynamicPackageData",
            "UserData"
        };

        private FolderArchitecture _folderArchitecture = new(ConsoleOutputConfig);

        public override bool Install()
        {
            foreach (var folderStructureName in _folderStructureNames)
            {
                string folderStructurePath = CreateFolderStructurePath(folderStructureName);

                if (_folderArchitecture.Generate(folderStructurePath) == false)
                    return false;
            }

            return true;
        }

        private string CreateFolderStructurePath(string name) =>
            PathToFolderWithStructures + name + Metadata.AssetExtension;
    }
}

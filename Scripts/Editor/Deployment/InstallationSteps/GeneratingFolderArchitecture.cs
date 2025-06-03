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
                if (GenerateFolderStructure(folderStructureName) == false)
                    return false;
            }

            return true;
        }

        private bool GenerateFolderStructure(string name)
        {
            string folderStructurePath = CreateFolderStructurePath(name);
            _folderArchitecture.Generate(folderStructurePath, out bool isSuccessful);

            return isSuccessful;
        }

        private string CreateFolderStructurePath(string name) =>
            PathToFolderWithStructures + name + Metadata.AssetExtension;
    }
}

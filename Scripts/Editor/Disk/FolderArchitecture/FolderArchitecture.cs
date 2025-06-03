using Serein.Reflection;
using System.IO;
using UnityEditor;

namespace Serein.Disk.Editor
{
    public class FolderArchitecture
    {
        private const string FoldersGeneratedMessagePart = 
            " folder structure generated successfully";

        private const string GitKeepFilesDeletedMessage = 
            Metadata.GitKeepFileName + " files deleted successfully";

        private const string FoldersGenerationError = 
            "Error generating folder structure";

        private const string GitKeepFileDeletionError = 
            "Error deleting " + Metadata.GitKeepFileName + " files";

        private readonly ConsoleOutputConfig _consoleOutputConfig;

        public FolderArchitecture() : 
            this(new(Package.ModuleName.Disk, typeof(FolderArchitecture))) { }

        public FolderArchitecture(ConsoleOutputConfig consoleOutputConfig)
        {
            _consoleOutputConfig = consoleOutputConfig;
        }

        public bool Generate(string pathToFolderStructure, bool createGitKeepFiles = true)
        {
            var folderStructureResource = Resource<FolderStructure>.Load(pathToFolderStructure);

            return folderStructureResource is not null
                ? Generate(folderStructureResource.Asset, createGitKeepFiles) 
                : false;
        }

        public bool Generate(FolderStructure folderStructure, bool createGitKeepFiles = true)
        {
            try
            {
                foreach (string folder in folderStructure.Folders)
                {
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);

                    if (createGitKeepFiles)
                        File.WriteAllText(Path.Combine(folder, Metadata.GitKeepFileName), string.Empty);
                }

                AssetDatabase.Refresh();
                Console.Log(folderStructure.name + FoldersGeneratedMessagePart, _consoleOutputConfig);

                return true;
            }
            catch
            {
                Console.LogError(FoldersGenerationError, _consoleOutputConfig);
                return false;
            }
        }

        public bool DeleteGitkeepFiles(string rootFolderName)
        {
            try
            {
                string[] gitkeepFiles = Directory.GetFiles(
                    rootFolderName, Metadata.GitKeepFileName, SearchOption.AllDirectories);

                foreach (string gitkeepFile in gitkeepFiles)
                    File.Delete(gitkeepFile);

                AssetDatabase.Refresh();
                Console.Log(GitKeepFilesDeletedMessage, _consoleOutputConfig);

                return true;
            }
            catch
            {
                Console.LogError(GitKeepFileDeletionError, _consoleOutputConfig);
                return false;
            }
        }
    }
}

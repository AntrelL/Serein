using Serein.Disk.Editor;
using Serein.Reflection;
using System.IO;
using UnityEditor;

namespace Serein.Deployment.Editor
{
    public class FolderArchitecture
    {
        private const string FoldersGeneratedMessagePart = 
            " folder structure generated successfully";

        private const string GitKeepFilesDeletedMessage = 
            Metadata.GitKeepFileName + " files deleted successfully";

        private const string FoldersGenerationError = "Error generating folder structure";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Deployment);

        public void Generate(
            string pathToFolderStructure, out bool isSuccessful, bool createGitKeepFiles = true)
        {
            var folderStructureResource = Resource<FolderStructure>.Load(pathToFolderStructure);
            isSuccessful = folderStructureResource is not null;

            if (isSuccessful == false)
                return;

            Generate(folderStructureResource.Asset, out isSuccessful, createGitKeepFiles);
        }

        public void Generate(
            FolderStructure folderStructure, out bool isSuccessful, bool createGitKeepFiles = true)
        {
            try
            {
                isSuccessful = true;

                foreach (string folder in folderStructure.Folders)
                {
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);

                    if (createGitKeepFiles)
                        File.WriteAllText(Path.Combine(folder, Metadata.GitKeepFileName), string.Empty);
                }

                AssetDatabase.Refresh();
                Console.Log(folderStructure.name + FoldersGeneratedMessagePart, s_consoleOutputConfig);
            }
            catch
            {
                Console.LogError(FoldersGenerationError, s_consoleOutputConfig);
                isSuccessful = false;
            }  
        }

        public void DeleteGitkeepFiles(string rootFolderName)
        {
            string[] gitkeepFiles = Directory.GetFiles(
                rootFolderName, Metadata.GitKeepFileName, SearchOption.AllDirectories);

            foreach (string gitkeepFile in gitkeepFiles)
                File.Delete(gitkeepFile);

            AssetDatabase.Refresh();
            Console.Log(GitKeepFilesDeletedMessage, s_consoleOutputConfig);
        }
    }
}

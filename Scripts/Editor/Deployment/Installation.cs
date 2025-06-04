using Serein.Reflection;
using System.Collections.Generic;
using UnityEditor;

namespace Serein.Deployment.Editor
{
    internal class Installation
    {
        private const string StartMessage = Package.Name + " installation started";
        private const string SuccessMessage = Package.Name + " installation complete";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Deployment, typeof(Installation));

        private readonly Contract _installationContract = 
            new(Package.Name + " is already installed", MessageSeverity.Info, s_consoleOutputConfig);

        private readonly Contract _stepContract =
            new("Installation failed", outputConfig: s_consoleOutputConfig);

        private List<InstallationStep> _steps = new()
        {
            new GeneratingFolderArchitecture(),
            new AddressablesInitialization(),
            new CreatingMainScene()
        };

        public static bool IsCompleted => AssetDatabase.IsValidFolder(Package.PathToDynamicDataFolder);

        public void Start()
        {
            if (_installationContract.CheckViolation(IsCompleted))
                return;

            Console.Log(StartMessage, s_consoleOutputConfig);

            foreach (var step in _steps)
            {
                if (_stepContract.CheckViolation(step.Install() == false))
                    return;
            }

            Console.Log(SuccessMessage, s_consoleOutputConfig);
        }
    }
}

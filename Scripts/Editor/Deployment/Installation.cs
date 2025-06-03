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

        private static readonly Contract s_installationContract = 
            new(Package.Name + " is already installed", MessageSeverity.Info, s_consoleOutputConfig);

        private static readonly Contract s_stepContract =
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
            if (s_installationContract.CheckViolation(IsCompleted))
                return;

            Console.Log(StartMessage, s_consoleOutputConfig);

            foreach (var step in _steps)
            {
                if (s_stepContract.CheckViolation(step.Install() == false))
                    return;
            }

            Console.Log(SuccessMessage, s_consoleOutputConfig);
        }
    }
}

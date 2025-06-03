using Serein.Reflection;
using UnityEditor.AddressableAssets;

namespace Serein.Deployment.Editor
{
    internal class AddressablesInitialization : InstallationStep
    {
        private const string AddressablesInitializedMessage = "Addressables initialized";

        private static readonly Contract s_initializationContract = 
            new("Failed to initialize addressables", outputConfig: ConsoleOutputConfig);

        public override bool Install()
        {
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);

            if (s_initializationContract.CheckViolation(settings is null))
                return false;

            settings.CreateGroup(Package.SystemAddressablesGroupName, false, false, false, null);

            Console.Log(AddressablesInitializedMessage, ConsoleOutputConfig);
            return true;
        }
    }
}

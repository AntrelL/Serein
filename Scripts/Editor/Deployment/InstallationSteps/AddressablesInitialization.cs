using Serein.Reflection;
using UnityEditor.AddressableAssets;

namespace Serein.Deployment.Editor
{
    internal class AddressablesInitialization : InstallationStep
    {
        private const string AddressablesInitializedMessage = "Addressables initialized";

        private readonly Contract _initializationContract = 
            new("Failed to initialize addressables", outputConfig: ConsoleOutputConfig);

        public override bool Install()
        {
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);

            if (_initializationContract.CheckViolation(settings is null))
                return false;

            settings.CreateGroup(Package.SystemAddressablesGroupName, false, false, false, null);

            Console.Log(AddressablesInitializedMessage, ConsoleOutputConfig);
            return true;
        }
    }
}

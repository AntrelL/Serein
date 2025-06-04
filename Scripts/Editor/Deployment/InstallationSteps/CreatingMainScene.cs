using Serein.Disk.Editor;
using Serein.Infrastructure;
using Serein.Reflection;
using UnityEngine;

namespace Serein.Deployment.Editor
{
    internal class CreatingMainScene : InstallationStep
    {
        private const string MainSceneCreatedMessage = "Main scene created";

        private static readonly Contract s_creationContract = 
            new("Failed to create main scene", outputConfig: ConsoleOutputConfig);

        public override bool Install()
        {
            Scene mainScene = new(Package.PathToMainScene);

            mainScene.AddObject((GameObject gameObject) =>
            {
                gameObject.AddComponent<EntryPoint>();
                gameObject.name = Package.SystemObjectName;
                gameObject.isStatic = true;

                return gameObject;
            });

            if (s_creationContract.CheckViolation(mainScene.Save() == false))
                return false;

            Console.Log(MainSceneCreatedMessage, ConsoleOutputConfig);
            return true;
        }
    }
}

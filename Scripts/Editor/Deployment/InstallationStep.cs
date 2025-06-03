using Serein.Reflection;

namespace Serein.Deployment.Editor
{
    internal abstract class InstallationStep
    {
        protected static readonly ConsoleOutputConfig ConsoleOutputConfig =
            new(Package.ModuleName.Deployment);

        public abstract bool Install();
    }
}

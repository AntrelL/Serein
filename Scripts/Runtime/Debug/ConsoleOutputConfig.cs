using System;

namespace Serein
{
    public struct ConsoleOutputConfig
    {
        public ConsoleOutputConfig(string moduleName = null, Type type = null) : 
            this(moduleName, type?.GetCorrectName()) { }

        public ConsoleOutputConfig(string moduleName, string typeName)
        {
            ModuleName = moduleName;
            TypeName = typeName;
        }

        public string ModuleName { get; private set; }

        public string TypeName { get; private set; }
    }
}

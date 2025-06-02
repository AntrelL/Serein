using System;

namespace Serein
{
    public struct ConsoleOutputConfig
    {
        public ConsoleOutputConfig(string moduleName = null, Type type = null) : 
            this(moduleName, type?.Name) { }

        public ConsoleOutputConfig(string moduleName = null, string typeName = null)
        {
            ModuleName = moduleName;
            TypeName = typeName;
        }

        public string ModuleName { get; private set; }

        public string TypeName { get; private set; }
    }
}

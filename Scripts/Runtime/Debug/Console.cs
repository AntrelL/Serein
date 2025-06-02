using System;
using System.Text;
using UnityEngine;

namespace Serein
{
    public static class Console
    {
        #region Log Methods
        public static void Log<T>(object @object, string moduleName = null) =>
            Write<T>(@object, moduleName, MessageSeverity.Info);

        public static void Log(object @object, ConsoleOutputConfig config) =>
            Write(@object, config, MessageSeverity.Info);

        public static void Log(object @object, string moduleName = null, string typeName = null) =>
            Write(@object, moduleName, typeName, MessageSeverity.Info);
        #endregion

        #region Log Warning Methods
        public static void LogWarning<T>(object @object, string moduleName = null) =>
            Write<T>(@object, moduleName, MessageSeverity.Warning);

        public static void LogWarning(object @object, ConsoleOutputConfig config) =>
            Write(@object, config, MessageSeverity.Warning);

        public static void LogWarning(object @object, string moduleName = null, string typeName = null) =>
            Write(@object, moduleName, typeName, MessageSeverity.Warning);
        #endregion

        #region Log Error Methods
        public static void LogError<T>(object @object, string moduleName = null) =>
            Write<T>(@object, moduleName, MessageSeverity.Error);

        public static void LogError(object @object, ConsoleOutputConfig config) =>
            Write(@object, config, MessageSeverity.Error);

        public static void LogError(object @object, string moduleName = null, string typeName = null) =>
            Write(@object, moduleName, typeName, MessageSeverity.Error);
        #endregion

        public static void Write<T>(
            object @object, string moduleName, MessageSeverity severity)
        {
            Write(@object, moduleName, typeof(T).Name, severity);
        }

        public static void Write(
            object @object, ConsoleOutputConfig config, MessageSeverity severity)
        {
            Write(@object, config.ModuleName, config.TypeName, severity);
        }

        public static void Write(
            object @object, string moduleName, string typeName, MessageSeverity severity)
        {
            string message = CreateCompositeMessage(@object.ToString(), moduleName, typeName);
            GetLogMethod(severity)(message);
        }

        private static Action<string> GetLogMethod(MessageSeverity severity) => severity switch
        {
            MessageSeverity.Info => Debug.Log,
            MessageSeverity.Warning => Debug.LogWarning,
            MessageSeverity.Error => Debug.LogError,
            _ => Debug.Log,
        };

        private static string CreateCompositeMessage(string message, string moduleName, string typeName)
        {
            bool isModuleName = string.IsNullOrEmpty(moduleName) == false;
            bool isTypeName = string.IsNullOrEmpty(typeName) == false;

            if ((isModuleName || isTypeName) == false)
                return message;

            StringBuilder completedMessage = new();

            completedMessage.Append(isModuleName ? $"[{moduleName}]" : string.Empty);
            completedMessage.Append(isTypeName ? " " + typeName : string.Empty);

            completedMessage.Append(completedMessage.Length > 0 ? ": " : string.Empty);
            completedMessage.Append(message);

            return completedMessage.ToString().Trim();
        }
    }
}

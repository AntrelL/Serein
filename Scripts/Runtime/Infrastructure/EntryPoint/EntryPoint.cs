using Serein.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Serein.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Infrastructure, typeof(EntryPoint));

        private readonly Contract _entryPointMethodPresenceContract =
            new("Entry point method not found", outputConfig: s_consoleOutputConfig);

        private readonly Contract _mainGameTypePresenceContract =
            new("Main game type not found", outputConfig: s_consoleOutputConfig);

        private readonly Contract _mainGameTypeCountContract = new(
            text: "Only one type should inherit from type " + nameof(Program),
            outputConfig: s_consoleOutputConfig);

        private void Awake()
        {
            HideSystemObject();
            List<Type> derivedTypes = GetDerivedTypes(typeof(Program));

            if (VerifyNumberOfMainGameTypes(derivedTypes) == false)
                return;

            if (TryGetEntryPointMethod(derivedTypes[0], out MethodInfo entryPointMethod) == false)
                return;

            entryPointMethod.Invoke(null, null);
        }

        private List<Type> GetDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsClass &&
                    type.IsAbstract == false &&
                    baseType.IsAssignableFrom(type))
                .ToList();
        }

        private bool VerifyNumberOfMainGameTypes(List<Type> derivedTypes)
        {
            if (_mainGameTypePresenceContract.CheckViolation(derivedTypes.Count == 0))
                return false;

            if (_mainGameTypeCountContract.CheckViolation(derivedTypes.Count > 1))
                return false;

            return true;
        }

        private bool TryGetEntryPointMethod(Type gameType, out MethodInfo entryPointMethod)
        {
            entryPointMethod = gameType.GetMethod(
                Metadata.EntryPointMethodName,
                Metadata.EntryPointMethodBindingFlags);

            bool isEntryPointMethodFound = 
                entryPointMethod is not null &&
                entryPointMethod.GetParameters().Length == 0;

            if (_entryPointMethodPresenceContract.CheckViolation(isEntryPointMethodFound == false))
                return false;

            return true;
        }

        private void HideSystemObject() => DontDestroyOnLoad(this);
    }
}

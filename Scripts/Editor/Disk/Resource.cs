using Serein.Reflection;
using System;
using UnityEditor;

namespace Serein.Disk.Editor
{
    public class Resource<T> where T : UnityEngine.Object
    {
        private static ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Disk, typeof(Resource<T>));

        private static Contract s_creationContract = new(
            text: "Error creating asset, asset with this name already exists, path: ",
            outputConfig: s_consoleOutputConfig);

        private static Contract s_loadingContract = new(
            text: "Failed to load asset at path: ", 
            outputConfig: s_consoleOutputConfig);

        private Resource(T asset, string path)
        {
            Asset = asset;
            Path = path;
        }

        public T Asset { get; private set; }

        public string Path { get; private set; }

        public static Resource<T> Create(string path, Func<T> assetCreator)
        {
            if (s_creationContract.CheckViolation(Exists(path), postfix: path))
                return null;

            T asset = assetCreator.Invoke();

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            return new(asset, path);
        }

        public static Resource<T> Load(string path, Func<T> fallbackAssetCreator = null)
        {
            if (TryLoad(path, out Resource<T> resource))
                return resource;

            if (s_loadingContract.CheckViolation(fallbackAssetCreator is null, postfix: path))
                return null;

            return Create(path, fallbackAssetCreator);
        }

        public static bool TryLoad(string path, out Resource<T> resource)
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);

            bool isSuccessful = asset is not null;
            resource = isSuccessful ? new(asset, path) : null;

            return isSuccessful;
        }

        public static bool Exists(string path) => TryLoad(path, out _);

        public void Edit(Action<T> changer, bool autoSave = true)
        {
            changer.Invoke(Asset);

            if (autoSave)
                Save();
        }

        public void Save()
        {
            EditorUtility.SetDirty(Asset);
            AssetDatabase.SaveAssets();
        }
    }
}

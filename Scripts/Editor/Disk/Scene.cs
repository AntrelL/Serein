using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityScene = UnityEngine.SceneManagement.Scene;

namespace Serein.Disk.Editor
{
    public class Scene
    {
        private UnityScene _unityScene;
        private string _path;

        public Scene(string path)
        {
            _unityScene = EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene, NewSceneMode.Single);

            _path = path;
        }

        public void AddObject(Func<GameObject, GameObject> objectInitializer)
        {
            SceneManager.MoveGameObjectToScene(
                objectInitializer.Invoke(new GameObject()), _unityScene);
        }

        public bool Save()
        {
            return EditorSceneManager.SaveScene(_unityScene, _path);
        }
    }
}

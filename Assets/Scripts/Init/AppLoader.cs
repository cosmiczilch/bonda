using TimiShared.Debug;
using TimiShared.Init;
using TimiShared.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppLoader : MonoBehaviour, IInitializable {

    [SerializeField] private SceneLoadData[] _scenesToLoad;

    private int _scenesLoadedCounter = 0;

    #region IInitializable
    public void StartInitialize() {
        this._scenesLoadedCounter = 0;
        for (int i = 0; i < this._scenesToLoad.Length; ++i) {
            SceneLoadData sceneToLoad = this._scenesToLoad[i];
            SceneLoader.Instance.LoadSceneAsync(sceneToLoad.sceneName, LoadSceneMode.Additive, this.SceneLoadCallback);
        }
    }

    public bool IsFullyInitialized {
        get; private set;
    }

    public string GetName {
        get {
            return this.GetType().Name;
        }
    }
    #endregion

    private void SceneLoadCallback(string sceneName, bool sceneLoadedSuccess) {
        if (sceneLoadedSuccess) {
            ++this._scenesLoadedCounter;
            if (this._scenesLoadedCounter >= this._scenesToLoad.Length) {
                this.IsFullyInitialized = true;
            }
        } else {
            TimiDebug.LogErrorColor("Failed to load scene: " + sceneName, LogColor.red);
        }
    }

    [System.Serializable]
    public class SceneLoadData {
        [SerializeField] public string sceneName;
    }
}

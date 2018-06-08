using TimiShared.Init;
using TimiShared.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppLoader : MonoBehaviour, IInitializable {

    [SerializeField] private string _mainSceneName;

    #region IInitializable
    public void StartInitialize() {
        SceneLoader.Instance.LoadSceneAsync(this._mainSceneName, LoadSceneMode.Additive, (bool sceneLoadSuccess) => {
            // TODO: Gracefully handle sceneLoadFailure here so that the app won't hang
            this.IsFullyInitialized = true;
        });
    }

    public bool IsFullyInitialized {
        get; private set;
    }
    #endregion
}

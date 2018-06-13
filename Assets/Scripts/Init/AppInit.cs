using System.Collections;
using TimiShared.Init;
using TimiShared.Service;
using UnityEngine;

public class AppInit : MonoBehaviour, IInitializable {

#region Events
    public static System.Action OnAppInitComplete = delegate {};
#endregion

    [SerializeField] private AppDataModel _appDataModel;

#region IInitializable
    public void StartInitialize() {
        this.StartCoroutine(this.InitializeAsync());
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

    private IEnumerator InitializeAsync() {
        yield return this._appDataModel.LoadDataAsync();
        ServiceLocator.RegisterService<AppDataModel>(this._appDataModel);
        // yield on more things or initialize other things here

        this.IsFullyInitialized = true;
        OnAppInitComplete.Invoke();
    }
}

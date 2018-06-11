using TimiShared.Init;
using TimiShared.Service;
using UnityEngine;

public class AppInit : MonoBehaviour, IInitializable {

#region Events
    public static System.Action OnAppInitComplete = delegate {};
#endregion

    // TODO: Move this somewhere else. This is too janky
    [SerializeField]
    private string _starsDataPath = "Assets/Resources/Data/starsData";

#region IInitializable
    public void StartInitialize() {
        AppDataModel appDataModel = new AppDataModel();
        appDataModel.LoadData(this._starsDataPath);
        ServiceLocator.RegisterService<AppDataModel>(appDataModel);

        this.IsFullyInitialized = true;
        OnAppInitComplete.Invoke();
    }

    public bool IsFullyInitialized {
        get; private set;
    }
#endregion
}

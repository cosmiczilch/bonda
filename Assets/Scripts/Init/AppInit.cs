using TimiShared.Debug;
using TimiShared.Init;
using TimiShared.Service;
using UnityEngine;

public class AppInit : MonoBehaviour, IInitializable {

#region Events
    public static System.Action OnAppInitComplete = delegate {};
#endregion

#region IInitializable
    public void StartInitialize() {
        // Register AppDataModel
        AppDataModel appDataModel = new AppDataModel();
        appDataModel.LoadData(() => {
            // TODO: Fix this flow please, we can't support multiple things being inited here in any non-ugly way right now
            ServiceLocator.RegisterService<AppDataModel>(appDataModel);
            this.IsFullyInitialized = true;
            OnAppInitComplete.Invoke();
        });
    }

    public bool IsFullyInitialized {
        get; private set;
    }
#endregion
}

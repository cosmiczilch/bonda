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

            StarsData starsData = AppDataModel.Instance.StarsData;
            if (starsData == null) {
                TimiDebug.LogColor("null", LogColor.cyan);
            }
            if (starsData != null && starsData.stars != null) {
                TimiDebug.LogColor("Loaded this many stars: " + starsData.stars.Count, LogColor.cyan);
            }
        });
    }

    public bool IsFullyInitialized {
        get; private set;
    }
#endregion
}

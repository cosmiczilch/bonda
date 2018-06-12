using TimiShared.Debug;
using UnityEngine;

public class Scratch : MonoBehaviour {

	// Use this for initialization
    void Start () {
        AppInit.OnAppInitComplete += this.OnAppInitComplete;
        this.OnAppInitComplete();
    }

    private void OnDestroy() {
        AppInit.OnAppInitComplete -= this.OnAppInitComplete;
	}

	private void OnAppInitComplete() {
        if (AppDataModel.Instance.StarsData == null || AppDataModel.Instance.StarsData.stars == null) {
            TimiDebug.LogErrorColor("No stars loaded", LogColor.red);
        }
        TimiDebug.LogColor("Loaded " + AppDataModel.Instance.StarsData.stars.Count + " stars", LogColor.cyan);
    }
}

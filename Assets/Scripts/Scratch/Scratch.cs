using TimiShared.Debug;
using UnityEngine;

public class Scratch : MonoBehaviour {

	// Use this for initialization
    private void Start () {
        if (AppDataModel.Instance.StarsData == null || AppDataModel.Instance.StarsData.stars == null) {
            TimiDebug.LogErrorColor("No stars loaded", LogColor.red);
        }
        TimiDebug.LogColor("Loaded " + AppDataModel.Instance.StarsData.stars.Count + " stars", LogColor.cyan);
    }
}

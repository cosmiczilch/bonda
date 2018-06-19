using TimiShared.Debug;
using UnityEngine;

public class Scratch : MonoBehaviour {

    [SerializeField] private float _apparentMagnitudeBrightest = -2.0f;
    [SerializeField] private float _apparentMagnitudeDimmest = 6;

	// Use this for initialization
    private void Start () {
        if (AppDataModel.Instance.StarsData == null || AppDataModel.Instance.StarsData.GetAllStars() == null) {
            TimiDebug.LogErrorColor("No stars loaded", LogColor.red);
        }
        TimiDebug.LogColor("Loaded " + AppDataModel.Instance.StarsData.GetAllStars().Count + " stars", LogColor.cyan);

        var enumerator = AppDataModel.Instance.StarsData.GetAllStars().GetEnumerator();
        while (enumerator.MoveNext()) {
            if (enumerator.Current.common_name.ToLower() == "sirius") {
               TimiDebug.LogColor(enumerator.Current.apparent_magnitude.ToString(), LogColor.cyan);
            }
//            if (!string.IsNullOrEmpty(enumerator.Current.common_name)) {
//                float apparentMagnitudeNormalized = Mathf.InverseLerp(this._apparentMagnitudeDimmest, this._apparentMagnitudeBrightest, enumerator.Current.apparent_magnitude);
//                TimiDebug.LogColor(enumerator.Current.common_name + ": " + enumerator.Current.apparent_magnitude + ", " + apparentMagnitudeNormalized, color);
//            }
        }

    }
}

using TimiShared.Debug;
using TimiShared.Loading;
using UnityEngine;

public class StarSpawner : MonoBehaviour {

    [SerializeField] private string _starPrefabPath;
    [SerializeField] private Transform _targetCamera;
    [SerializeField] private int _numStars = 10;
    [SerializeField] private float _starfieldDistance = 1200;

    private void Start() {
        if (string.IsNullOrEmpty(this._starPrefabPath)) {
            TimiDebug.LogWarning("Star prefab path not set");
            return;
        }
        if (this._targetCamera == null) {
            TimiDebug.LogWarning("Target camera not set");
            return;
        }

        this.SpawnStars();
    }

    private void Update() {
//        this.transform.RotateAround(this.transform.position, this.transform.right, -0.02f);
    }

    private void SpawnStars() {
        if (AppDataModel.Instance.StarsData == null ||
        AppDataModel.Instance.StarsData.stars == null ||
        AppDataModel.Instance.StarsData.stars.Count == 0) {
            return;
        }

        StarData someStar = null;
        var enumerator = AppDataModel.Instance.StarsData.stars.GetEnumerator();
        while (enumerator.MoveNext()) {
            if (true || !string.IsNullOrEmpty(enumerator.Current.common_name)) {
                someStar = enumerator.Current;

                GameObject starGO = PrefabLoader.Instance.InstantiateSynchronous(this._starPrefabPath, this.transform);

                starGO.transform.localPosition = StarFieldUtils.GetPositionFromRaDec(someStar.right_ascension,
                                                                someStar.declination, this._starfieldDistance);
                if (!string.IsNullOrEmpty(someStar.common_name)) {
                    starGO.name = someStar.common_name;
                    if (someStar.common_name == "Polaris" || someStar.common_name == "Mintaka") {
                        starGO.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                    } else {
                        starGO.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    }
                }

                Lookat starLookat = starGO.GetComponent<Lookat>();
                if (starLookat != null) {
                    starLookat.Target = this._targetCamera;
                }
            }
        }
    }


}

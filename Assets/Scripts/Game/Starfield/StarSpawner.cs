using TimiShared.Loading;
using TimiShared.Debug;
using UnityEngine;

// TODO: Remove this class
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

        // this.SpawnGridLikeThing();
        this.SpawnStars();
    }

    private void Update() {
//        this.transform.RotateAround(this.transform.position, this.transform.right, -0.02f);
    }

    private void SpawnStars() {

        StarData someStar = null;
        var enumerator = AppDataModel.Instance.StarsData.GetStarsWithCommonNames().GetEnumerator();
        while (enumerator.MoveNext()) {
            if (!string.IsNullOrEmpty(enumerator.Current.common_name)) {
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

    private void SpawnGridLikeThing() {

        int howManyAround = 18;
        for (int j = 0; j < howManyAround; ++j) {

            int howManyUp = 10;
            for (int i = 0; i < howManyUp; ++i) {

                float phi = j * 2 * Mathf.PI / howManyAround;
                float theta = i * Mathf.PI / 2.0f / howManyUp;

                GameObject starGO = PrefabLoader.Instance.InstantiateSynchronous(this._starPrefabPath, this.transform);

                starGO.transform.localPosition = StarFieldUtils.GetPositionFromPhiTheta(phi, theta, this._starfieldDistance);
                starGO.name = i.ToString();
                starGO.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);

                Lookat starLookat = starGO.GetComponent<Lookat>();
                if (starLookat != null) {
                    starLookat.Target = this._targetCamera;
                }
            }
        }
    }




}

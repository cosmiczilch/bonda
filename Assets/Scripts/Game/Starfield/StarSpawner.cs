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
        this.transform.RotateAround(this.transform.position, this.transform.right, -0.02f);
    }

    private void SpawnStars() {
        Random.InitState(200);
        for (int i = 0; i < this._numStars; ++i) {
            PrefabLoader.Instance.InstantiateAsynchronous(this._starPrefabPath, this.transform, (GameObject starGO) => {
                float theta = Random.Range(0.0f, 360.0f * Mathf.PI / 180.0f);
                float phi   = Random.Range(0.0f, 360.0f * Mathf.PI / 180.0f);
                float x = this._starfieldDistance * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = this._starfieldDistance * Mathf.Sin(theta) * Mathf.Sin(phi);
                float z = this._starfieldDistance * Mathf.Cos(theta) * -1;
                starGO.transform.localPosition = new Vector3(x, y, z);

                Lookat starLookat = starGO.GetComponent<Lookat>();
                if (starLookat != null) {
                    starLookat.Target = this._targetCamera;
                }
            });
        }
    }


}

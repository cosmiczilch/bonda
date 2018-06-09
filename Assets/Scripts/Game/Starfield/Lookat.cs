using TimiShared.Debug;
using UnityEngine;

[ExecuteInEditMode]
public class Lookat : MonoBehaviour {

    [SerializeField]
    private Transform _target;
    public Transform Target {
        get {
            return this._target;
        }
        set {
            this._target = value;
            this.UpdateOrientation();
        }
    }

    [SerializeField]
    private bool _updateContinuously = false;

    private void Start() {
        if (this._target != null) {
            this.UpdateOrientation();
        } else {
            TimiDebug.LogWarning("Lookat target not set");
        }
    }

    private void Update() {
        if (this._updateContinuously && this._target != null) {
            this.UpdateOrientation();
        }
    }

    private void UpdateOrientation() {
        this.transform.LookAt(this._target);
    }
}

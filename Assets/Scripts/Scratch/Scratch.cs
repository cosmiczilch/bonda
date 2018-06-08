using TimiShared.Debug;
using UnityEngine;

public class Scratch : MonoBehaviour {

    [SerializeField]
    private Transform _blinkingCubeTransform;

    [SerializeField]
    private OVRCameraRig _cameraRig;

	// Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
    void Update () {
        bool isButtonDown = OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two);

        if (this._blinkingCubeTransform != null) {
            this._blinkingCubeTransform.gameObject.SetActive(!isButtonDown);
        }

        if (this._cameraRig != null) {
            if (this._cameraRig.usePerEyeCameras == false && isButtonDown) {
                TimiDebug.LogErrorColor("Switching", LogColor.red);
            }
            this._cameraRig.usePerEyeCameras = isButtonDown;
        }
    }
}

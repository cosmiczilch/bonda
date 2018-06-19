using System.Runtime.Remoting.Messaging;
using UnityEngine;

public static class StarFieldUtils {

    /*
     Phi, Theta are in radians
     TODO: Add documentation somewhere for how phi, theta are measured and link here
     */
    public static Vector3 GetPositionFromRaDec(float rightAscension,
                                            float declination,
                                            float starFieldDistance) {
        return GetPositionFromPhiTheta( GetPhiFromRightAscension(rightAscension),
                                        GetThetaFromDeclination(declination),
                                        starFieldDistance);
    }

    /*
     Phi, Theta are in radians
     TODO: Add documentation somewhere for how phi, theta are measured and link here
     */
    public static Vector3 GetPositionFromPhiTheta(float phi, float theta, float starFieldDistance) {
        Vector3 result = new Vector3();

        result.x = starFieldDistance * Mathf.Sin(phi) * Mathf.Cos(theta);
        result.z = starFieldDistance * -Mathf.Cos(phi) * Mathf.Cos(theta);
        result.y = starFieldDistance * Mathf.Sin(theta);

        return result;
    }

    // Phi in radians
    public static float GetPhiFromRightAscension(float rightAscension) {
        return rightAscension * 2.0f * Mathf.PI / 24.0f;

    }

    // Theta in radians
    public static float GetThetaFromDeclination(float declination) {
        return declination * (Mathf.PI / 2.0f) / 90.0f;
    }


}

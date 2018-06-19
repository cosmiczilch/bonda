using UnityEngine;

public static class StarFieldUtils {

    public static Vector3 GetPositionFromRaDec(float rightAscension,
                                            float declination,
                                            float starFieldDistance) {
        Vector3 result = new Vector3();
        float phi = rightAscension * 2.0f * Mathf.PI / 24.0f;
        float theta = declination * (Mathf.PI / 2.0f) / 90.0f;
        result.x = starFieldDistance * Mathf.Sin(phi) * Mathf.Cos(theta);
        result.z = starFieldDistance * -Mathf.Cos(phi) * Mathf.Cos(theta);
        result.y = starFieldDistance * Mathf.Sin(theta);

        return result;
    }


}

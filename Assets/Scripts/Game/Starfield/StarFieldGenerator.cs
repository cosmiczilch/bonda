using System.Collections.Generic;
using TimiShared.Debug;
using UnityEngine;

public class StarFieldGenerator : MonoBehaviour {

    [SerializeField] private Transform _harness;
    [SerializeField] private MeshFilter _meshFilter;

    private const float STARFIELD_DISTANCE = 2000.0f;

    // TODO: Get this from StarsData
    [SerializeField] private float _apparentMagnitudeBrightest = -2.0f;
    [SerializeField] private float _apparentMagnitudeDimmest = 6;

    // TODO: Make this more intuitive
    private float _scaleBrightest = 100.0f;
    private float _scaleDimmest = 50.0f;

    private Vector3 topright = new Vector3(0.5f, 0.5f, 0.0f);
    private Vector3 topleft = new Vector3(-0.5f, 0.5f, 0.0f);
    private Vector3 bottomleft = new Vector3(-0.5f, -0.5f, 0.0f);
    private Vector3 bottomright = new Vector3(0.5f, -0.5f, 0.0f);

    private void Start() {
        if (this._harness == null) {
            TimiDebug.LogWarningColor("Harness not set", LogColor.orange);
            GameObject.Destroy(this);
            return;
        }
        if (this._meshFilter == null) {
            TimiDebug.LogWarningColor("Mesh Filter not set", LogColor.orange);
            GameObject.Destroy(this);
            return;
        }

        this.GenerateStars(this._meshFilter);
//        this.GenerateOneStar(this._meshFilter);
    }

    // TODO: Remove. This is only for debugging the star position calculations
    private void GenerateOneStar(MeshFilter meshFilter) {
        StarData star = AppDataModel.Instance.StarsData.GetStarDataByCommonName("Megrez");
        TimiDebug.LogColor(star.right_ascension + " " + star.declination, LogColor.black);
        int numStars = 1;
        Vector3[] vertices = new Vector3[4 * numStars];
        int[] triangles = new int[6 * numStars];
        Vector2[] uvs = new Vector2[4 * numStars];

        GenerateVerticesForOneStar(star, 0, vertices, triangles, uvs);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        meshFilter.mesh = mesh;
    }

    private void GenerateStars(MeshFilter meshFilter) {
        List<StarData> starsToRender = AppDataModel.Instance.StarsData.GetAllStars();
        // List<StarData> starsToRender = AppDataModel.Instance.StarsData.GetStarsWithCommonNames();

        int numStars = starsToRender.Count;
        Vector3[] vertices = new Vector3[4 * numStars];
        int[] triangles = new int[6 * numStars];
        Vector2[] uvs = new Vector2[4 * numStars];

        int starIndex = 0;
        var enumerator = starsToRender.GetEnumerator();
        while (enumerator.MoveNext()) {
            StarData currentStar = enumerator.Current;
            GenerateVerticesForOneStar(currentStar, starIndex, vertices, triangles, uvs);
            ++starIndex;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        meshFilter.mesh = mesh;
    }

    private void GenerateVerticesForOneStar(StarData starData, int starIndex,
                                            Vector3[] vertices, int[] triangles, Vector2[] uvs) {

        float apparentMagnitudeNormalized = Mathf.InverseLerp(this._apparentMagnitudeDimmest, this._apparentMagnitudeBrightest, starData.apparent_magnitude);
        float scaleByBrightness = Mathf.Lerp(this._scaleDimmest, this._scaleBrightest, apparentMagnitudeNormalized) / 2.0f;

        // TODO: Remove extra scaling for stars with common names
        if (!string.IsNullOrEmpty(starData.common_name)) {
            scaleByBrightness *= 2;
        }

        Vector3 position = StarFieldUtils.GetPositionFromRaDec(starData.right_ascension, starData.declination, STARFIELD_DISTANCE);
        Quaternion quaternion = Quaternion.LookRotation(position - this.transform.position);
        Vector3 scale = new Vector3(scaleByBrightness, scaleByBrightness, scaleByBrightness);

        Matrix4x4 transformMatrix = Matrix4x4.TRS(position, quaternion, scale);

        vertices[starIndex * 4 + 0] = transformMatrix.MultiplyPoint3x4(topright);
        vertices[starIndex * 4 + 1] = transformMatrix.MultiplyPoint3x4(topleft);
        vertices[starIndex * 4 + 2] = transformMatrix.MultiplyPoint3x4(bottomleft);
        vertices[starIndex * 4 + 3] = transformMatrix.MultiplyPoint3x4(bottomright);

        triangles[starIndex * 6 + 0] = starIndex * 4 + 0;
        triangles[starIndex * 6 + 1] = starIndex  *4 + 2;
        triangles[starIndex * 6 + 2] = starIndex  *4 + 1;
        //
        triangles[starIndex * 6 + 3] = starIndex * 4 + 0;
        triangles[starIndex * 6 + 4] = starIndex * 4 + 3;
        triangles[starIndex * 6 + 5] = starIndex * 4 + 2;

        uvs[starIndex * 4 + 0] = new Vector2(1.0f, 1.0f);
        uvs[starIndex * 4 + 1] = new Vector2(0.0f, 1.0f);
        uvs[starIndex * 4 + 2] = new Vector2(0.0f, 0.0f);
        uvs[starIndex * 4 + 3] = new Vector2(1.0f, 0.0f);

        // TODO: Add normals if we are going to use any lighting on these?
    }


}

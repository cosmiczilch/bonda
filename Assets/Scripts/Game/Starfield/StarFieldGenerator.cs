using TimiShared.Debug;
using UnityEngine;

public class StarFieldGenerator : MonoBehaviour {

    [SerializeField] private Transform _harness;
    [SerializeField] private MeshFilter _meshFilter;

    private const float STARFIELD_DISTANCE = 2000.0f;

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

        this.GenerateSimpleMesh(this._meshFilter);
    }

    private void GenerateSimpleMesh(MeshFilter meshFilter) {
        if (AppDataModel.Instance.StarsData == null ||
            AppDataModel.Instance.StarsData.stars == null ||
            AppDataModel.Instance.StarsData.stars.Count == 0) {
            return;
        }

        StarData someStar = null;
        var enumerator = AppDataModel.Instance.StarsData.stars.GetEnumerator();
        while (enumerator.MoveNext()) {
//            if (!string.IsNullOrEmpty(enumerator.Current.common_name)) {
//                TimiDebug.LogColor(enumerator.Current.common_name + ", ra: " + enumerator.Current.right_ascension.ToString()
//                + ", dec: " + enumerator.Current.declination.ToString(), LogColor.maroon);
//            }
            if (enumerator.Current.common_name == "Sirius") {
                someStar = enumerator.Current;
                 break;
            }
        }

        if (someStar == null) {
            return;
        }

        Vector3 position = StarFieldUtils.GetPositionFromRaDec(someStar.right_ascension, someStar.declination, STARFIELD_DISTANCE);

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0.5f, 0.5f, 0.0f);
        vertices[1] = new Vector3(-0.5f, 0.5f, 0.0f);
        vertices[2] = new Vector3(-0.5f, -0.5f, 0.0f);
        vertices[3] = new Vector3(0.5f, -0.5f, 0.0f);

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        //
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        // TODO: Add normals if we are going to use any lighting on these?

        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(1.0f, 1.0f);
        uvs[1] = new Vector2(0.0f, 1.0f);
        uvs[2] = new Vector2(0.0f, 0.0f);
        uvs[3] = new Vector2(1.0f, 0.0f);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        meshFilter.mesh = mesh;
    }


}

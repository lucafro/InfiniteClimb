// NON-PERLIN NOISE APPROACH
//using MyBox;
//using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
//public class MeshGenerator : MonoBehaviour
//{
//    [SerializeField]
//    private int _xResolution = 10;
//    [SerializeField]
//    private int _zResolution = 10;
//    [SerializeField]
//    private float _width = 5.0f;
//    [SerializeField]
//    private float _depth = 5.0f;
//    [SerializeField]
//    private float _levelOfDisplacement = 2.0f;
//    [SerializeField]
//    private MeshFilter _meshFilter;

//    private void Awake()
//    {
//        if (_meshFilter == null)
//            _meshFilter = GetComponent<MeshFilter>();
//    }

//    private void Start()
//    {
//        Generate();
//    }

//    [ButtonMethod]
//    public void Generate()
//    {
//        RemoveMesh();
//        _meshFilter.mesh = GenerateMesh(_xResolution, _zResolution, _width, _depth, _levelOfDisplacement);
//    }

//    public Mesh GenerateMesh(int xResolution, int zResolution, float width, float depth, float levelOfDisplacement)
//    {
//        Mesh mesh = new Mesh();

//        int verticesPerLineX = xResolution + 1;
//        int verticesPerLineZ = zResolution + 1;
//        Vector3[] vertices = new Vector3[verticesPerLineX * verticesPerLineZ];
//        int[] triangles = new int[xResolution * zResolution * 6];
//        Vector2[] uvs = new Vector2[vertices.Length];

//        int vertexIndex = 0;
//        int triangleIndex = 0;

//        for (int z = 0; z < verticesPerLineZ; z++)
//        {
//            for (int x = 0; x < verticesPerLineX; x++)
//            {
//                float y = Random.Range(-levelOfDisplacement, levelOfDisplacement);
//                float xPos = (float)x / xResolution * width;
//                float zPos = (float)z / zResolution * depth;
//                vertices[vertexIndex] = new Vector3(xPos, y, zPos);

//                if (x < xResolution && z < zResolution)
//                {
//                    int start = vertexIndex;
//                    triangles[triangleIndex] = start;
//                    triangles[triangleIndex + 1] = start + verticesPerLineX;
//                    triangles[triangleIndex + 2] = start + 1;
//                    triangles[triangleIndex + 3] = start + 1;
//                    triangles[triangleIndex + 4] = start + verticesPerLineX;
//                    triangles[triangleIndex + 5] = start + verticesPerLineX + 1;

//                    triangleIndex += 6;
//                }

//                uvs[vertexIndex] = new Vector2(xPos / width, zPos / depth);

//                vertexIndex++;
//            }
//        }

//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.uv = uvs;
//        mesh.RecalculateNormals();

//        return mesh;
//    }

//    [ButtonMethod]
//    public void RemoveMesh()
//    {
//        if (_meshFilter.sharedMesh != null)
//        {
//            _meshFilter.sharedMesh.Clear();
//        }
//    }
//}





// PERLIN NOISE APPROACH
//using UnityEngine;
//using MyBox;

//[RequireComponent(typeof(MeshFilter))]
//public class MeshGenerator : MonoBehaviour
//{
//    [SerializeField]
//    private int _xResolution = 10;
//    [SerializeField]
//    private int _zResolution = 10;
//    [SerializeField]
//    private float _width = 5.0f;
//    [SerializeField]
//    private float _depth = 5.0f;
//    [SerializeField]
//    private float _levelOfDisplacement = 2.0f;
//    [SerializeField]
//    private float _noiseScale = 0.3f;

//    private MeshFilter _meshFilter;

//    private void Awake()
//    {
//        _meshFilter = GetComponent<MeshFilter>();
//    }

//#if UNITY_EDITOR
//    [ButtonMethod]
//#endif
//    public void Generate()
//    {
//        _meshFilter.sharedMesh = GenerateMesh(_xResolution, _zResolution, _width, _depth, _levelOfDisplacement, _noiseScale);
//    }

//    private Mesh GenerateMesh(int xResolution, int zResolution, float width, float depth, float levelOfDisplacement, float noiseScale)
//    {
//        Mesh mesh = new Mesh();
//        mesh.name = "ProceduralMesh";

//        int verticesPerLineX = xResolution + 1;
//        int verticesPerLineZ = zResolution + 1;
//        Vector3[] vertices = new Vector3[verticesPerLineX * verticesPerLineZ];
//        int[] triangles = new int[xResolution * zResolution * 6];
//        Vector2[] uvs = new Vector2[vertices.Length];

//        int vertexIndex = 0;
//        int triangleIndex = 0;

//        for (int z = 0; z < verticesPerLineZ; z++)
//        {
//            for (int x = 0; x < verticesPerLineX; x++)
//            {
//                float xPos = (float)x / xResolution * width;
//                float zPos = (float)z / zResolution * depth;

//                // Apply Perlin noise based displacement
//                float y = Mathf.PerlinNoise(xPos * noiseScale, zPos * noiseScale) * levelOfDisplacement;

//                vertices[vertexIndex] = new Vector3(xPos, y, zPos);

//                if (x < xResolution && z < zResolution)
//                {
//                    int start = vertexIndex;
//                    triangles[triangleIndex] = start;
//                    triangles[triangleIndex + 1] = start + verticesPerLineX;
//                    triangles[triangleIndex + 2] = start + 1;
//                    triangles[triangleIndex + 3] = start + 1;
//                    triangles[triangleIndex + 4] = start + verticesPerLineX;
//                    triangles[triangleIndex + 5] = start + verticesPerLineX + 1;

//                    triangleIndex += 6;
//                }

//                uvs[vertexIndex] = new Vector2(xPos / width, zPos / depth);

//                vertexIndex++;
//            }
//        }

//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.uv = uvs;
//        mesh.RecalculateNormals();

//        return mesh;
//    }

//#if UNITY_EDITOR
//    [ButtonMethod]
//#endif
//    public void RemoveMesh()
//    {
//        if (_meshFilter.sharedMesh != null)
//        {
//            _meshFilter.sharedMesh.Clear();
//        }
//    }
//}




//MIX OF PERLIN NOISE AND RANDOM DISPLACEMENT
//using UnityEngine;
//using MyBox;
//using UnityEditor;

//[RequireComponent(typeof(MeshFilter))]
//public class MeshGenerator : MonoBehaviour
//{
//    [SerializeField]
//    private int _xResolution = 10;
//    [SerializeField]
//    private int _zResolution = 10;
//    [SerializeField]
//    private float _width = 5.0f;
//    [SerializeField]
//    private float _depth = 5.0f;
//    [SerializeField]
//    private float _levelOfDisplacement = 2.0f;
//    [SerializeField]
//    private float _noiseScale = 0.3f;
//    [SerializeField]
//    private float _randomDisplacementIntensity = 0.5f; // New field to control the intensity of random displacement

//    [SerializeField]
//    private MeshFilter _meshFilter;

//    private void Awake()
//    {
//        if(_meshFilter == null)
//        _meshFilter = GetComponent<MeshFilter>();
//    }

//    [ButtonMethod]
//    public void SaveMeshAsAsset()
//    {
//        Mesh mesh = _meshFilter.sharedMesh;
//        if (mesh != null)
//        {
//            string path = EditorUtility.SaveFilePanelInProject("Save Mesh", "ProceduralMesh", "asset", "Please enter a file name to save the mesh to");
//            if (path.Length != 0)
//            {
//                AssetDatabase.CreateAsset(mesh, path);
//                AssetDatabase.SaveAssets();
//            }
//        }
//        else
//        {
//            Debug.LogError("Mesh is null. Generate the mesh before trying to save.");
//        }
//    }

//    [ButtonMethod]
//    public void Generate()
//    {
//        _meshFilter.sharedMesh = GenerateMesh(_xResolution, _zResolution, _width, _depth, _levelOfDisplacement, _noiseScale, _randomDisplacementIntensity);
//    }

//    private Mesh GenerateMesh(int xResolution, int zResolution, float width, float depth, float levelOfDisplacement, float noiseScale, float randomDisplacementIntensity)
//    {
//        Mesh mesh = new Mesh();
//        mesh.name = "ProceduralMesh";

//        int verticesPerLineX = xResolution + 1;
//        int verticesPerLineZ = zResolution + 1;
//        Vector3[] vertices = new Vector3[verticesPerLineX * verticesPerLineZ];
//        int[] triangles = new int[xResolution * zResolution * 6];
//        Vector2[] uvs = new Vector2[vertices.Length];

//        int vertexIndex = 0;
//        int triangleIndex = 0;

//        for (int z = 0; z < verticesPerLineZ; z++)
//        {
//            for (int x = 0; x < verticesPerLineX; x++)
//            {
//                float xPos = (float)x / xResolution * width;
//                float zPos = (float)z / zResolution * depth;

//                // Mix of Perlin noise and random displacement
//                float noise = Mathf.PerlinNoise(xPos * noiseScale, zPos * noiseScale) * levelOfDisplacement;
//                float randomDisplacement = Random.Range(-randomDisplacementIntensity, randomDisplacementIntensity);
//                float y = noise + randomDisplacement;

//                vertices[vertexIndex] = new Vector3(xPos, y, zPos);

//                if (x < xResolution && z < zResolution)
//                {
//                    int start = vertexIndex;
//                    triangles[triangleIndex] = start;
//                    triangles[triangleIndex + 1] = start + verticesPerLineX;
//                    triangles[triangleIndex + 2] = start + 1;
//                    triangles[triangleIndex + 3] = start + 1;
//                    triangles[triangleIndex + 4] = start + verticesPerLineX;
//                    triangles[triangleIndex + 5] = start + verticesPerLineX + 1;

//                    triangleIndex += 6;
//                }

//                uvs[vertexIndex] = new Vector2(xPos / width, zPos / depth);

//                vertexIndex++;
//            }
//        }

//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.uv = uvs;
//        mesh.RecalculateNormals();

//        return mesh;
//    }

//#if UNITY_EDITOR
//    [ButtonMethod]
//#endif
//    public void RemoveMesh()
//    {
//        if (_meshFilter.sharedMesh != null)
//        {
//            _meshFilter.sharedMesh.Clear();
//        }
//    }
//}



using UnityEngine;
using MyBox;  // Assuming you're using MyBox for the ButtonMethod attribute
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private int _xResolution = 10;
    [SerializeField]
    private int _zResolution = 10;
    [SerializeField]
    private float _width = 5.0f;
    [SerializeField]
    private float _depth = 5.0f;
    [SerializeField]
    private float _levelOfDisplacement = 2.0f;
    [SerializeField]
    private float _noiseScale = 0.3f;
    [SerializeField]
    private float _randomDisplacementIntensity = 0.5f;
    [SerializeField]
    private float _displacementScale = 1.0f; // Renamed for clarity and to act as an overall scaling factor

    [SerializeField]
    private MeshFilter _meshFilter;

    Vector2 uvs;

    private void Awake()
    {
        if (_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();
    }

    [ButtonMethod]
    public void SaveMeshAsAsset()
    {
        Mesh mesh = _meshFilter.sharedMesh;
        if (mesh != null)
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Mesh", "ProceduralMesh", "asset", "Please enter a file name to save the mesh to");
            if (path.Length != 0)
            {
                AssetDatabase.CreateAsset(mesh, path);
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            Debug.LogError("Mesh is null. Generate the mesh before trying to save.");
        }
    }

    [ButtonMethod]
    public void Generate()
    {
        _meshFilter.sharedMesh = GenerateMesh(_xResolution, _zResolution, _width, _depth, _levelOfDisplacement, _noiseScale, _randomDisplacementIntensity, _displacementScale);
    }

    private Mesh GenerateMesh(int xResolution, int zResolution, float width, float depth, float levelOfDisplacement, float noiseScale, float randomDisplacementIntensity, float displacementScale)
    {
        Mesh mesh = new()
        {
            name = "ProceduralMesh"
        };

        int verticesPerLineX = xResolution + 1;
        int verticesPerLineZ = zResolution + 1;
        Vector3[] vertices = new Vector3[verticesPerLineX * verticesPerLineZ];
        int[] triangles = new int[xResolution * zResolution * 6];
        Vector2[] uvs = new Vector2[vertices.Length];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < verticesPerLineZ; z++)
        {
            for (int x = 0; x < verticesPerLineX; x++)
            {
                float xPos = (float)x / xResolution * width;
                float zPos = (float)z / zResolution * depth;

                // Adjusting displacement with the scale factor
                float noise = Mathf.PerlinNoise(xPos * noiseScale, zPos * noiseScale) * levelOfDisplacement * displacementScale;
                float randomDisplacement = Random.Range(-randomDisplacementIntensity, randomDisplacementIntensity) * displacementScale;
                float y = noise + randomDisplacement;

                vertices[vertexIndex] = new Vector3(xPos, y, zPos);

                if (x < xResolution && z < zResolution)
                {
                    int start = vertexIndex;
                    triangles[triangleIndex] = start;
                    triangles[triangleIndex + 1] = start + verticesPerLineX;
                    triangles[triangleIndex + 2] = start + 1;
                    triangles[triangleIndex + 3] = start + 1;
                    triangles[triangleIndex + 4] = start + verticesPerLineX;
                    triangles[triangleIndex + 5] = start + verticesPerLineX + 1;

                    triangleIndex += 6;
                }

                uvs[vertexIndex] = new Vector2(xPos / width, zPos / depth);

                vertexIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }

    [ButtonMethod]
    public void RemoveMesh()
    {

        if (_meshFilter.sharedMesh != null)
        {
            _meshFilter.sharedMesh.Clear();
        }
    }
}
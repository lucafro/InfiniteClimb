using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TransformProvider : MonoBehaviour
{
    public enum PlaneAxis
    {
        XY,
        XZ,
        YZ
    }

    [SerializeField] float planeHeight = 0f; // Height of the plane
    [SerializeField] float spawnWidth = 10f; // Width of the spawn area
    [SerializeField] float spawnHeight = 10f; // Height of the spawn area
    [SerializeField] PlaneAxis planeAxis = PlaneAxis.XY;

    public UnityEvent<Transform> TransformCreated = new();
    public UnityEvent TransformDestroyed = new();

    private void OnDrawGizmos()
    {
        // Draw the plane as a Gizmo
        DrawPlaneGizmo();
    }

    private void DrawPlaneGizmo()
    {
        Gizmos.color = Color.green;
        Vector3 planeCenter = GetPlaneCenter();

        Vector3 planeSize = GetPlaneSize();

        Gizmos.DrawWireCube(planeCenter, planeSize);
    }

    private Vector3 GetPlaneCenter()
    {
        switch (planeAxis)
        {
            case PlaneAxis.XY:
                return new Vector3(transform.position.x, transform.position.y + planeHeight, transform.position.z);
            case PlaneAxis.XZ:
                return new Vector3(transform.position.x, transform.position.y, transform.position.z + planeHeight);
            case PlaneAxis.YZ:
                return new Vector3(transform.position.x + planeHeight, transform.position.y, transform.position.z);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetPlaneSize()
    {
        float sizeX = 0f;
        float sizeY = 0f;
        float sizeZ = 0f;

        switch (planeAxis)
        {
            case PlaneAxis.XY:
                sizeX = spawnWidth;
                sizeY = 0.1f;
                sizeZ = spawnHeight;
                break;
            case PlaneAxis.XZ:
                sizeX = spawnWidth;
                sizeY = spawnHeight;
                sizeZ = 0.1f;
                break;
            case PlaneAxis.YZ:
                sizeX = 0.1f;
                sizeY = spawnHeight;
                sizeZ = spawnWidth;
                break;
        }

        return new Vector3(sizeX, sizeY, sizeZ);
    }


    private Vector3 GetSpawnPosition(Vector2 randomPosition)
    {
        switch (planeAxis)
        {
            case PlaneAxis.XY:
                return new Vector3(randomPosition.x, planeHeight, randomPosition.y) + transform.position;
            case PlaneAxis.XZ:
                return new Vector3(randomPosition.x, randomPosition.y, planeHeight) + transform.position;
            case PlaneAxis.YZ:
                return new Vector3(planeHeight, randomPosition.y, randomPosition.x) + transform.position;
            default:
                return Vector3.zero;
        }
    }

    public void CreateRandomTransform()
    {
        GetRandomTransform();
    }

    public Transform GetRandomTransform()
    {
        // Get a random position on the plane
        Vector2 randomPosition = new Vector2(Random.Range(-spawnWidth / 2, spawnWidth / 2), Random.Range(-spawnHeight / 2, spawnHeight / 2));
        Vector3 spawnPosition = GetSpawnPosition(randomPosition);

        //Create a new GameObject and set its position
        GameObject newObject = new GameObject("RandomTransformObject");
        newObject.transform.parent = transform;
        newObject.transform.position = spawnPosition;

        TransformCreated?.Invoke(newObject.transform);
        // Return the Transform component of the new GameObject
        if (gameObject.activeInHierarchy)
            StartCoroutine(WaitAndRemoveTransform(newObject));
        return newObject.transform;
    }

    private IEnumerator WaitAndRemoveTransform(GameObject gameObjectToRemove)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObjectToRemove);
        TransformDestroyed?.Invoke();
    }



}

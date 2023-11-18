using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZonesCreator : MonoBehaviour
{
    public enum PlaneAxis
    {
        XY,
        XZ,
        YZ
    }

    [Header("Spawn Area")]
    [SerializeField]
    private PlaneAxis _planeAxis = PlaneAxis.XY;

    [SerializeField]
    private float _planeHeight = 0f;

    [SerializeField]
    private float _spawnWidth = 10f;

    [SerializeField]
    private float _spawnHeight = 10f;

    [SerializeField]
    private float _yOffset = 0f;

    [Header("Spawn Settings")]
    [SerializeField]
    private GameObject _spawnZonePrefab;

    [SerializeField, MaxValue(500)]
    private int _spawnAmount = 10;

    [Space(10)]
    [SerializeField]
    private bool _randomizeHorizontalOffset = false;

    [SerializeField, ConditionalField(nameof(_randomizeHorizontalOffset)), Range(1, 5)]
    private float _horizontalOffsetFactor = 0.5f;

    [SerializeField]
    private bool _avoidOverlappingZones = true;

    private List<GameObject> _spawnedZones = new List<GameObject>();

    public void OnDrawGizmosSelected()
    {
        DrawPlaneGizmo();
    }

    private void DrawPlaneGizmo()
    {
        Gizmos.color = Color.magenta;
        Vector3 planeCenter = GetPlaneCenter();
        Vector3 planeSize = GetPlaneSize();
        Gizmos.DrawWireCube(planeCenter, planeSize);
    }

    private Vector3 GetPlaneCenter()
    {
        return _planeAxis switch
        {
            PlaneAxis.XY => new Vector3(transform.position.x, transform.position.y + _planeHeight + _yOffset, transform.position.z),
            PlaneAxis.XZ => new Vector3(transform.position.x, transform.position.y + _yOffset, transform.position.z + _planeHeight),
            PlaneAxis.YZ => new Vector3(transform.position.x + _planeHeight, transform.position.y + _yOffset, transform.position.z),
            _ => Vector3.zero,
        };
    }

    private Vector3 GetPlaneSize()
    {
        float sizeX = 0f;
        float sizeY = 0f;
        float sizeZ = 0f;

        switch (_planeAxis)
        {
            case PlaneAxis.XY:
                sizeX = _spawnWidth;
                sizeY = 0.1f;
                sizeZ = _spawnHeight;
                break;
            case PlaneAxis.XZ:
                sizeX = _spawnWidth;
                sizeY = _spawnHeight;
                sizeZ = 0.1f;
                break;
            case PlaneAxis.YZ:
                sizeX = 0.1f;
                sizeY = _spawnHeight;
                sizeZ = _spawnWidth;
                break;
        }

        return new Vector3(sizeX, sizeY, sizeZ);
    }

    [ButtonMethod]
    public void CreateSpawnZones()
    {
        RemoveSpawnedZones();
        _spawnedZones = new List<GameObject>();

        Vector3 planeCenter = GetPlaneCenter();
        Vector3 planeSize = GetPlaneSize();
        float verticalSpacing = planeSize.y / _spawnAmount;

        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnPosition = CalculateSpawnPosition(planeCenter, planeSize, verticalSpacing, i);
            GameObject spawnZone = Instantiate(_spawnZonePrefab, spawnPosition, Quaternion.identity, transform);
            _spawnedZones.Add(spawnZone);

            if (_avoidOverlappingZones)
                CheckAndRemoveOverlappingZones();
        }
    }

    [ButtonMethod]
    public void RemoveSpawnedZones()
    {
        foreach (var zone in _spawnedZones)
        {
            DestroyImmediate(zone);
        }
        _spawnedZones.Clear();
    }

    private void CheckAndRemoveOverlappingZones()
    {
        for (int i = 0; i < _spawnedZones.Count; i++)
        {
            for (int j = i + 1; j < _spawnedZones.Count; j++)
            {
                if (CheckOverlap(_spawnedZones[i], _spawnedZones[j]))
                {
                    DestroyImmediate(_spawnedZones[j]);
                    _spawnedZones.RemoveAt(j);
                    j--; // Adjust the index because we removed an item
                }
            }
        }
    }

    private bool CheckOverlap(GameObject zone1, GameObject zone2)
    {
        Collider collider1 = zone1.GetComponent<Collider>();
        Collider collider2 = zone2.GetComponent<Collider>();

        if (collider1 != null && collider2 != null)
        {
            return collider1.bounds.Intersects(collider2.bounds);
        }

        return false;
    }

    private Vector3 CalculateSpawnPosition(Vector3 planeCenter, Vector3 planeSize, float verticalSpacing, int index)
    {
        if (_randomizeHorizontalOffset)
        {
            float randomXOffset = Random.Range(-planeSize.x / (15 / _horizontalOffsetFactor), planeSize.x / (15 / _horizontalOffsetFactor));
            float randomZOffset = Random.Range(-planeSize.z / (15 / _horizontalOffsetFactor), planeSize.z / (15 / _horizontalOffsetFactor));
            return new Vector3(
                planeCenter.x + randomXOffset,
                planeCenter.y - (planeSize.y / 2) + index * verticalSpacing,
                planeCenter.z + randomZOffset
            );
        }
        else
        {
            return new Vector3(
                planeCenter.x,
                planeCenter.y - (planeSize.y / 2) + index * verticalSpacing,
                planeCenter.z
            );
        }
    }
}



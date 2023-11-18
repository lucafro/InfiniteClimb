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
    [SerializeField]
    private bool _randomizeHorizontalOffset = false;

    private List<GameObject> spawnedZones = new List<GameObject>();
    private float _xOffset = 0f;

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
        switch (_planeAxis)
        {
            case PlaneAxis.XY:
                return new Vector3(transform.position.x, transform.position.y + _planeHeight + _yOffset, transform.position.z);
            case PlaneAxis.XZ:
                return new Vector3(transform.position.x, transform.position.y + _yOffset, transform.position.z + _planeHeight);
            case PlaneAxis.YZ:
                return new Vector3(transform.position.x + _planeHeight, transform.position.y + _yOffset, transform.position.z);
            default:
                return Vector3.zero;
        }
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

    //[ButtonMethod]
    //public void CreateSpawnZones()
    //{
    //    RemoveSpawnedZones();
    //    spawnedZones = new List<GameObject>();

    //    Vector3 planeCenter = GetPlaneCenter();
    //    Vector3 planeSize = GetPlaneSize();

    //    float verticalSpacing = planeSize.y / _spawnAmount;

    //    for (int i = 0; i < _spawnAmount; i++)
    //    {
    //        Vector3 spawnPosition = Vector3.one;
    //        if (_randomizeHorizontalOffset)
    //        {
    //            _xOffset = Random.Range(-planeSize.x + (planeSize.x / 2), planeSize.x - (planeSize.x / 2));
    //            spawnPosition = new Vector3(planeCenter.x + _xOffset, -_yOffset + planeCenter.y + i * verticalSpacing, planeCenter.z);
    //        }
    //        else
    //        {
    //            spawnPosition = new Vector3(planeCenter.x, -_xOffset + planeCenter.y + i * verticalSpacing, planeCenter.z);

    //        }
    //        GameObject spawnZone = Instantiate(_spawnZonePrefab, spawnPosition, Quaternion.identity, transform);

    //        spawnedZones.Add(spawnZone);
    //    }
    //}

    //[ButtonMethod]
    //public void CreateSpawnZones()
    //{
    //    RemoveSpawnedZones();
    //    spawnedZones = new List<GameObject>();

    //    Vector3 planeCenter = GetPlaneCenter();
    //    Vector3 planeSize = GetPlaneSize();

    //    float verticalSpacing = planeSize.y / _spawnAmount;

    //    for (int i = 0; i < _spawnAmount; i++)
    //    {
    //        Vector3 spawnPosition;

    //        if (_randomizeHorizontalOffset)
    //        {
    //            float randomXOffset = Random.Range(-planeSize.x / 2, planeSize.x / 2);
    //            float randomZOffset = Random.Range(-planeSize.z / 2, planeSize.z / 2);

    //            spawnPosition = new Vector3(
    //                planeCenter.x + randomXOffset,
    //                planeCenter.y + i * verticalSpacing,
    //                planeCenter.z + randomZOffset
    //            );
    //        }
    //        else
    //        {
    //            spawnPosition = new Vector3(
    //                planeCenter.x,
    //                planeCenter.y + i * verticalSpacing,
    //                planeCenter.z
    //            );
    //        }

    //        GameObject spawnZone = Instantiate(_spawnZonePrefab, spawnPosition, Quaternion.identity, transform);
    //        spawnedZones.Add(spawnZone);
    //    }
    //}


    [ButtonMethod]
    public void CreateSpawnZones()
    {
        RemoveSpawnedZones();
        spawnedZones = new List<GameObject>();

        Vector3 planeCenter = GetPlaneCenter();
        Vector3 planeSize = GetPlaneSize();

        float verticalSpacing = planeSize.y / _spawnAmount;

        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnPosition;

            if (_randomizeHorizontalOffset)
            {
                float randomXOffset = Random.Range(-planeSize.x / 2, planeSize.x / 2);
                float randomZOffset = Random.Range(-planeSize.z / 2, planeSize.z / 2);

                spawnPosition = new Vector3(
                    planeCenter.x + randomXOffset,
                    planeCenter.y - (planeSize.y / 2) + i * verticalSpacing,
                    planeCenter.z + randomZOffset
                );
            }
            else
            {
                spawnPosition = new Vector3(
                    planeCenter.x,
                    planeCenter.y - (planeSize.y / 2) + i * verticalSpacing,
                    planeCenter.z
                );
            }

            GameObject spawnZone = Instantiate(_spawnZonePrefab, spawnPosition, Quaternion.identity, transform);
            spawnedZones.Add(spawnZone);
        }
    }

    [ButtonMethod]
    public void RemoveSpawnedZones()
    {
        foreach (var zone in spawnedZones)
        {
            DestroyImmediate(zone);
        }
        spawnedZones.Clear();
    }
}



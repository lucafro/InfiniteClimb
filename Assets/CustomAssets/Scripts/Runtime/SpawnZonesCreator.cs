//using MyBox;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnZonesCreator : MonoBehaviour
//{
//    public enum PlaneAxis
//    {
//        XY,
//        XZ,
//        YZ
//    }

//    [Header("Spawn Area")]
//    [SerializeField]
//    private PlaneAxis _planeAxis = PlaneAxis.XY;
//    [SerializeField]
//    private float _planeHeight = 0f;
//    [SerializeField]
//    private float _spawnWidth = 10f;
//    [SerializeField]
//    private float _spawnHeight = 10f;
//    [SerializeField]
//    private float _yOffset = 0f;

//    [Header("Spawn Settings")]
//    [SerializeField]
//    private GameObject _spawnZonePrefab;
//    [SerializeField, MaxValue(500)]
//    private int _spawnAmount = 10;
//    [SerializeField]
//    private bool _avoidOverlappingZones = true;

//    [Space(10)]
//    [SerializeField]
//    private bool _randomizeHorizontalOffset = false;
//    [SerializeField, ConditionalField(nameof(_randomizeHorizontalOffset)), Range(0.1f, 10)]
//    private float _horizontalOffsetFactor = 0.5f;

//    private List<GameObject> _spawnedZones = new List<GameObject>();

//    public void OnDrawGizmosSelected()
//    {
//        DrawPlaneGizmo();
//    }

//    private void DrawPlaneGizmo()
//    {
//        Gizmos.color = Color.magenta;
//        Vector3 planeCenter = GetPlaneCenter();
//        Vector3 planeSize = GetPlaneSize();
//        Gizmos.DrawWireCube(planeCenter, planeSize);
//    }

//    private Vector3 GetPlaneCenter()
//    {
//        return _planeAxis switch
//        {
//            PlaneAxis.XY => new Vector3(transform.position.x, transform.position.y + _planeHeight + _yOffset, transform.position.z),
//            PlaneAxis.XZ => new Vector3(transform.position.x, transform.position.y + _yOffset, transform.position.z + _planeHeight),
//            PlaneAxis.YZ => new Vector3(transform.position.x + _planeHeight, transform.position.y + _yOffset, transform.position.z),
//            _ => Vector3.zero,
//        };
//    }

//    private Vector3 GetPlaneSize()
//    {
//        float sizeX = 0f;
//        float sizeY = 0f;
//        float sizeZ = 0f;

//        switch (_planeAxis)
//        {
//            case PlaneAxis.XY:
//                sizeX = _spawnWidth;
//                sizeY = 0.1f;
//                sizeZ = _spawnHeight;
//                break;
//            case PlaneAxis.XZ:
//                sizeX = _spawnWidth;
//                sizeY = _spawnHeight;
//                sizeZ = 0.1f;
//                break;
//            case PlaneAxis.YZ:
//                sizeX = 0.1f;
//                sizeY = _spawnHeight;
//                sizeZ = _spawnWidth;
//                break;
//        }

//        return new Vector3(sizeX, sizeY, sizeZ);
//    }

//    [ButtonMethod]
//    public void CreateSpawnZones()
//    {
//        RemoveSpawnedZones();
//        _spawnedZones = new List<GameObject>();

//        Vector3 planeCenter = GetPlaneCenter();
//        Vector3 planeSize = GetPlaneSize();
//        float verticalSpacing = planeSize.y / _spawnAmount;

//        for (int i = 0; i < _spawnAmount; i++)
//        {
//            Vector3 spawnPosition = CalculateSpawnPosition(planeCenter, planeSize, verticalSpacing, i);
//            GameObject spawnZone = Instantiate(_spawnZonePrefab, spawnPosition, Quaternion.identity, transform);
//            _spawnedZones.Add(spawnZone);

//            if (_avoidOverlappingZones)
//                CheckAndRemoveOverlappingZones();
//        }
//    }

//    [ButtonMethod]
//    public void RemoveSpawnedZones()
//    {
//        foreach (var zone in _spawnedZones)
//        {
//            DestroyImmediate(zone);
//        }
//        _spawnedZones.Clear();
//    }

//    private void CheckAndRemoveOverlappingZones()
//    {
//        for (int i = 0; i < _spawnedZones.Count; i++)
//        {
//            for (int j = i + 1; j < _spawnedZones.Count; j++)
//            {
//                if (CheckOverlap(_spawnedZones[i], _spawnedZones[j]))
//                {
//                    DestroyImmediate(_spawnedZones[j]);
//                    _spawnedZones.RemoveAt(j);
//                    j--; // Adjust the index because we removed an item
//                }
//            }
//        }
//    }

//    private bool CheckOverlap(GameObject zone1, GameObject zone2)
//    {
//        Collider collider1 = zone1.GetComponent<Collider>();
//        Collider collider2 = zone2.GetComponent<Collider>();

//        if (collider1 != null && collider2 != null)
//        {
//            return collider1.bounds.Intersects(collider2.bounds);
//        }

//        return false;
//    }

//    private Vector3 CalculateSpawnPosition(Vector3 planeCenter, Vector3 planeSize, float verticalSpacing, int index)
//    {
//        if (_randomizeHorizontalOffset)
//        {
//            float randomXOffset = Random.Range(-planeSize.x / (15 / _horizontalOffsetFactor), planeSize.x / (15 / _horizontalOffsetFactor));
//            float randomZOffset = Random.Range(-planeSize.z / (15 / _horizontalOffsetFactor), planeSize.z / (15 / _horizontalOffsetFactor));
//            return new Vector3(
//                planeCenter.x + randomXOffset,
//                planeCenter.y - (planeSize.y / 2) + index * verticalSpacing,
//                planeCenter.z + randomZOffset
//            );
//        }
//        else
//        {
//            return new Vector3(
//                planeCenter.x,
//                planeCenter.y - (planeSize.y / 2) + index * verticalSpacing,
//                planeCenter.z
//            );
//        }
//    }
//}

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
    private bool _avoidOverlappingZones = true;

    [Space(10)]
    [SerializeField]
    private bool _randomizeHorizontalOffset = false;
    [SerializeField, ConditionalField(nameof(_randomizeHorizontalOffset)), Range(0.1f, 10)]
    private float _horizontalOffsetFactor = 0.5f;

    [SerializeField]
    private bool _randomizeVerticalOffset = false;
    [SerializeField, ConditionalField(nameof(_randomizeVerticalOffset)), Range(0.1f, 10)]
    private float _verticalOffsetFactor = 0.5f;

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

    //private void CheckAndRemoveOverlappingZones()
    //{
    //    for (int i = 0; i < _spawnedZones.Count; i++)
    //    {
    //        for (int j = i + 1; j < _spawnedZones.Count; j++)
    //        {
    //            if (CheckOverlap(_spawnedZones[i], _spawnedZones[j]))
    //            {
    //                DestroyImmediate(_spawnedZones[j]);
    //                _spawnedZones.RemoveAt(j);
    //                j--; // Adjust the index because we removed an item
    //            }
    //        }
    //    }
    //}


    private void CheckAndRemoveOverlappingZones()
    {
        List<GameObject> zonesToRemove = new List<GameObject>();

        for (int i = 0; i < _spawnedZones.Count; i++)
        {
            for (int j = i + 1; j < _spawnedZones.Count; j++)
            {
                if (CheckOverlap(_spawnedZones[i], _spawnedZones[j]))
                {
                    zonesToRemove.Add(_spawnedZones[j]);
                }
            }
        }

        foreach (var zone in zonesToRemove)
        {
            _spawnedZones.Remove(zone);
            DestroyImmediate(zone);
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
                GetVerticalSpawnPosition(planeCenter, planeSize, index),
                planeCenter.z + randomZOffset
            );
        }
        else
        {
            return new Vector3(
                planeCenter.x,
                GetVerticalSpawnPosition(planeCenter, planeSize, index),
                planeCenter.z
            );
        }
    }

    private float GetVerticalSpawnPosition(Vector3 planeCenter, Vector3 planeSize, int index)
    {
        float verticalSpacing = planeSize.y / _spawnAmount;
        if (_randomizeVerticalOffset)
        {
            float randomYOffset = Random.Range(-planeSize.y / (15 / _verticalOffsetFactor), planeSize.y / (15 / _verticalOffsetFactor));
            return planeCenter.y - (planeSize.y / 2) + index * verticalSpacing + randomYOffset;
        }
        else
        {
            return planeCenter.y - (planeSize.y / 2) + index * verticalSpacing;
        }
    }

    //private bool IsOutOfBounds(Vector3 position, Vector3 planeCenter, Vector3 planeSize)
    //{
    //    switch (_planeAxis)
    //    {
    //        case PlaneAxis.XY:
    //            return position.x < planeCenter.x - planeSize.x / 2 || position.x > planeCenter.x + planeSize.x / 2 ||
    //                   position.y < planeCenter.y - planeSize.y / 2 || position.y > planeCenter.y + planeSize.y / 2;
    //        case PlaneAxis.XZ:
    //            return position.x < planeCenter.x - planeSize.x / 2 || position.x > planeCenter.x + planeSize.x / 2 ||
    //                   position.z < planeCenter.z - planeSize.z / 2 || position.z > planeCenter.z + planeSize.z / 2;
    //        case PlaneAxis.YZ:
    //            return position.y < planeCenter.y - planeSize.y / 2 || position.y > planeCenter.y + planeSize.y / 2 ||
    //                   position.z < planeCenter.z - planeSize.z / 2 || position.z > planeCenter.z + planeSize.z / 2;
    //        default:
    //            return false;
    //    }
    //}
    private bool IsOutOfBounds(Vector3 position, Vector3 planeCenter, Vector3 planeSize)
    {
        switch (_planeAxis)
        {
            case PlaneAxis.XY:
                return position.x < planeCenter.x - planeSize.x / 2 || position.x > planeCenter.x + planeSize.x / 2 ||
                       position.y < planeCenter.y - planeSize.y / 2 || position.y > planeCenter.y + planeSize.y / 2;
            case PlaneAxis.XZ:
                return position.x < planeCenter.x - planeSize.x / 2 || position.x > planeCenter.x + planeSize.x / 2 ||
                       position.y < planeCenter.y - planeSize.y / 2 || position.y > planeCenter.y + planeSize.y / 2 ||
                       position.z < planeCenter.z - planeSize.z / 2 || position.z > planeCenter.z + planeSize.z / 2;
            case PlaneAxis.YZ:
                return position.y < planeCenter.y - planeSize.y / 2 || position.y > planeCenter.y + planeSize.y / 2 ||
                       position.z < planeCenter.z - planeSize.z / 2 || position.z > planeCenter.z + planeSize.z / 2;
            default:
                return false;
        }
    }


    [ButtonMethod]
    public void CheckOutOfBounds()
    {
        Vector3 planeCenter = GetPlaneCenter();
        Vector3 planeSize = GetPlaneSize();

        foreach (var zone in _spawnedZones)
        {
            if (zone == null)
                return;

            Vector3 zonePosition = zone.transform.position;

            if (IsOutOfBounds(zonePosition, planeCenter, planeSize))
            {
                Debug.LogWarning("Spawned zone is out of bounds! Removing...");
                // Optionally, you can handle or correct the out-of-bounds position here
                DestroyImmediate(zone);

            }
        }
    }
}




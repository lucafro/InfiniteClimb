using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

public class SpawnZone : MonoBehaviour
{
    [SerializeField, MyBox.ReadOnly]
    private GameObject _mainCamera;

    public UnityEvent<Transform> AbovePlayer, BelowPlayer;


    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void CheckIfAbovePlayer(Transform transform)
    {
        if (_mainCamera == null)
        {
            Debug.LogError("Player is null.", gameObject);
            return;
        }

        if (transform.position.y >= _mainCamera.transform.position.y)
        {
            AbovePlayer?.Invoke(transform);

        }
        else
        {
            BelowPlayer?.Invoke(transform);

        }
    }

}

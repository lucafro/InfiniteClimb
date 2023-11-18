using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectReferenceHandler : MonoBehaviour
{
    public GameObjectReference GameObjectReference;
    public UnityEvent<GameObject> Started;

    public void SetGameObject(GameObject gameObject)
    {
        GameObjectReference.Value = gameObject;
        //ReferenceReceived?.Invoke(GameObjectReference.Value);
    }

    public GameObject GetGameObject()
    {
        if (GameObjectReference == null)
        {
            Debug.LogError("GameObjectReference is null.", gameObject);
            return null;
        }
        else
            return GameObjectReference.Value;
    }

    private void Start()
    {
        if (GameObjectReference.Value != null)
        {
            Started?.Invoke(GameObjectReference.Value);
        }
        else
        {
            Debug.LogWarning("GameObjectReference.Value is null.", gameObject);
        }
    }
}

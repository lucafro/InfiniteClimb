using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectReference", menuName = "Scriptable Objects/Variables/GameObject Reference", order = 1)]
public class GameObjectReference : ScriptableObject
{
    [SerializeField]
    private GameObject _gameObject;

    public GameObject Value
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }
}
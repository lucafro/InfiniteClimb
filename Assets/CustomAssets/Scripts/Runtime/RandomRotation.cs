using MyBox;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [MinMaxRange(1, 360), SerializeField]
    private RangedFloat _rotationX = new(1, 360);
    [MinMaxRange(1, 360), SerializeField]
    private RangedFloat _rotationY = new(1, 360);
    [MinMaxRange(1, 360), SerializeField]
    private RangedFloat _rotationZ = new(1, 360);

    private void Start()
    {
        SetRandomRotation();
    }

    public void SetRandomRotation()
    {
        transform.Rotate(Random.Range(_rotationX.Min, _rotationX.Max), Random.Range(_rotationY.Min, _rotationY.Max), Random.Range(_rotationZ.Min, _rotationZ.Max));
    }
}

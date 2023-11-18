using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class RandomScale : MonoBehaviour
{
    [MinMaxRange(0.1f, 2.5f), SerializeField]
    private RangedFloat _scale = new RangedFloat(0.4f, 1.7f);

    private void Start()
    {
        transform.localScale = Vector3.one * Random.Range(_scale.Min, _scale.Max);
    }
}


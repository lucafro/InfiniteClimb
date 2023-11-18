using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDissolveValue : MonoBehaviour
{
    private List<Material> _materials = new();
    [SerializeField]
    private float _fadeInSpeed = 0.7f;

    void Awake()
    {
        CollectMaterials();
    }

    void CollectMaterials()
    {
        var renders = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renders)
        {
            _materials.AddRange(renderer.materials);
        }
    }

    public void FadeIn()
    {
            StartCoroutine(FadeInRoutine(_fadeInSpeed));
    }

    private IEnumerator FadeInRoutine(float fadeDuration)
    {
        float elapsedTime = 0f;
        //float fadeDuration = 1f; // You can adjust the duration as needed

        while (elapsedTime < fadeDuration)
        {
            float value = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            SetValue(value);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final value is set to avoid floating-point precision issues
        SetValue(0f);
    }

    public void SetValue(float value)
    {
        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetFloat("_Dissolve", value);
        }
    }
}

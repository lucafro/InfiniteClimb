using System.Collections;
using UnityEngine;

public class LightIntensityLerp : MonoBehaviour
{
    public float LerpTime = 2f;

    private Light _lightComponent;
    private float _targetIntensity;

    private void Awake()
    {
        _lightComponent = GetComponent<Light>();

        if (_lightComponent != null)
        {
            _targetIntensity = _lightComponent.intensity;
            _lightComponent.intensity = 0f;

            StartCoroutine(LerpIntensity());
        }
        else
        {
            Debug.LogError("Light component not found on the GameObject.");
        }
    }

    private IEnumerator LerpIntensity()
    {
        float elapsedTime = 0f;

        while (elapsedTime < LerpTime)
        {
            _lightComponent.intensity = Mathf.Lerp(0f, _targetIntensity, elapsedTime / LerpTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _lightComponent.intensity = _targetIntensity;
    }
}

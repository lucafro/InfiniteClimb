using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerGroupTransition : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _audioMixerGroup;

    [SerializeField]
    private float _fadeDuration = 20f;

    private float _originalVolume;

    [SerializeField]
    private bool _fadeInOnStart = true;

    private void Awake()
    {
        if (_audioMixerGroup == null)
        {
            Debug.LogError("Audio Mixer Group is not assigned!");
            return;
        }

        _audioMixerGroup.audioMixer.GetFloat(_audioMixerGroup.name + "Volume", out _originalVolume);

    }

    private void Start()
    {
        if (_fadeInOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name + "Volume", -80f);
        StartCoroutine(FadeVolume(-79f, _originalVolume, _fadeDuration));
    }

    public void FadeOut()
    {

        StartCoroutine(FadeVolume(GetVolume(), -80f, _fadeDuration));
    }

    public float GetVolume()
    {
        if (_audioMixerGroup == null)
        {
            Debug.LogError("Audio Mixer Group is not assigned!");
            return 0f;
        }

        float volume;
        _audioMixerGroup.audioMixer.GetFloat(_audioMixerGroup.name + "Volume", out volume);
        return volume;
    }

    private IEnumerator FadeVolume(float startVolume, float targetVolume, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

            _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name + "Volume", newVolume);

            yield return null;
        }

        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name + "Volume", targetVolume);
    }
}

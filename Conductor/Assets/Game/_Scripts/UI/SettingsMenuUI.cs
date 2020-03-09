using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] PostProcessVolume processVolume;
    [SerializeField] AudioMixer mixer;

    Bloom bloom = null;
    // Start is called before the first frame update
    void Start()
    {
        processVolume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBloom(float newBloomValue)
    {
        bloom.intensity.value = newBloomValue;
    }

    public void UpdateMasterVolume(float newMasterVolume)
    {
        Debug.Log(mixer);
        mixer.SetFloat("VolumeMaster", Mathf.Log10(newMasterVolume) * 20);
    }

    public void UpdateMusicVolume(float newMusicVolume)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Log10(newMusicVolume) * 20);
    }

    public void UpdateEffectsVolume(float newEffectsVolume)
    {
        mixer.SetFloat("VolumeEffects", Mathf.Log10(newEffectsVolume) * 20);
    }
}

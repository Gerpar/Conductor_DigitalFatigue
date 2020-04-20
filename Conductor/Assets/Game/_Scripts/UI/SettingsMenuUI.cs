using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] PostProcessVolume processVolume;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider effectsVolSlider;
    [SerializeField] Slider bloomVisSlider;

    Bloom bloom = null;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        processVolume.profile.TryGetSettings(out bloom);
        SetSliderPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBloom(float newBloomValue)
    {
        if (bloom != null)
        {
            bloom.intensity.value = newBloomValue;
            PlayerPrefs.SetFloat("BloomSliderVal", newBloomValue);
        }
    }

    // Gerad Paris
    public void UpdateMasterVolume(float newMasterVolume)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(newMasterVolume) * 20);
        PlayerPrefs.SetFloat("VolumeMasterSliderVal", newMasterVolume);
    }

    // Gerad Paris
    public void UpdateMusicVolume(float newMusicVolume)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Log10(newMusicVolume) * 20);
        PlayerPrefs.SetFloat("VolumeMusicSliderVal", newMusicVolume);
    }

    // Gerad Paris
    public void UpdateEffectsVolume(float newEffectsVolume)
    {
        mixer.SetFloat("VolumeEffects", Mathf.Log10(newEffectsVolume) * 20);
        PlayerPrefs.SetFloat("VolumeEffectsSliderVal", newEffectsVolume);
    }

    public void SetSliderPositions()
    {
        if (PlayerPrefs.HasKey("BloomSliderVal"))
        {
            float bloomVal = PlayerPrefs.GetFloat("BloomSliderVal");
            bloom.intensity.value = bloomVal;
            bloomVisSlider.value = bloomVal;
        }
        if (PlayerPrefs.HasKey("VolumeMasterSliderVal"))
        {
            float masVolVal = PlayerPrefs.GetFloat("VolumeMasterSliderVal");
            mixer.SetFloat("VolumeMaster", Mathf.Log10(masVolVal) * 20);
            masterVolSlider.value = masVolVal;
        }
        if (PlayerPrefs.HasKey("VolumeMusicSliderVal"))
        {
            float musVolVal = PlayerPrefs.GetFloat("VolumeMusicSliderVal");
            mixer.SetFloat("VolumeMusic", Mathf.Log10(musVolVal) * 20);
            musicVolSlider.value = musVolVal;
        }
        if (PlayerPrefs.HasKey("VolumeEffectsSliderVal"))
        {
            float effVolVal = PlayerPrefs.GetFloat("VolumeEffectsSliderVal");
            mixer.SetFloat("VolumeEffects", Mathf.Log10(effVolVal) * 20);
            effectsVolSlider.value = effVolVal;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] PostProcessVolume processVolume;

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
}

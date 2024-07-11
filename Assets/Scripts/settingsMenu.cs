using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class settingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private void Start()
    {
        float volumeLevel;
        audioMixer.GetFloat("volume",out volumeLevel);
        volumeSlider.value = volumeLevel;
    }
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}

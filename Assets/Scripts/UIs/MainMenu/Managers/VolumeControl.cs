using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BladesOfDeceptionCapstoneProject
{
    public class VolumeControl : MonoBehaviour
    {
        public AudioMixer audioMixer;  // Reference to the Audio Mixer
        public Slider volumeSlider;    // Reference to the Slider UI element

        void Start()
        {
            // Initialize slider value based on the current volume
            

            // Add a listener to the slider to call SetVolume when the value changes
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Method to set the volume
        void SetVolume(float volume)
        {
            // Convert the slider value to logarithmic scale and set the AudioMixer volume
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Use the exact name of the exposed parameter
        }
    }
}

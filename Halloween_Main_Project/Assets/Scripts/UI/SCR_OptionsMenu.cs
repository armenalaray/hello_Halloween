    using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SCR_OptionsMenu : MonoBehaviour {

    public Slider volumeSlider;
    public float masterVolume;


	
	// Update is called once per frame
	void Update () {

	}
    public void masterVolumeSlide()
    {
        masterVolume = volumeSlider.value;
        AudioListener.volume = masterVolume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    private float volume;
    private AudioSource audioSource;

	void Start () {
        volume = PlayerPrefs.GetFloat("VolumeMemory", 1f);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
	
	public void changeVolume(float vol)
    {
        audioSource.volume = vol;
        PlayerPrefs.SetFloat("VolumeMemory", vol);
    }

}

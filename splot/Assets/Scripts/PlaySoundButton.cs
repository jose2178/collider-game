using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundButton : MonoBehaviour {

    private AudioSource audioSource;

    // Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void ReproduceSonido(AudioClip sonido)
    {
        audioSource.clip = sonido;
        audioSource.Play();
    }
}

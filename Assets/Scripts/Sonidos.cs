using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public static Sonidos controlSonidos;
    private AudioSource sonido;

    private void Awake() {

        if(controlSonidos == null)
        {
            controlSonidos = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sonido = GetComponent<AudioSource>();
    }

    public void Reproducir(AudioClip musica)
    {
        sonido.PlayOneShot(musica);
    }
}

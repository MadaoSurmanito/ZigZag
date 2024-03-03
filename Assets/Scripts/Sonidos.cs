using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sonidos : MonoBehaviour
{
    public static Sonidos controlSonidos;
    private AudioSource sonido;
    public AudioClip musicaJuego;
    public AudioClip musicaFinal;

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

    public void MusicaFondo(int eleccion)
    {
        if(eleccion == 1)
        {
            sonido.loop = true;
            sonido.clip = musicaJuego;
            sonido.volume = 0.1f;
            sonido.Play();
        }
        else
        {
            sonido.loop = true;
            sonido.clip = musicaFinal;
            sonido.volume = 0.1f;
            sonido.Play();
        }
    }

    public void Detener()
    {
        sonido.Stop();
    }
}

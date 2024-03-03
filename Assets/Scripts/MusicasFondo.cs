using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicasFondo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Musicas empezando
        if(SceneManager.GetActiveScene().name == "Inicio")
        {
            Sonidos.controlSonidos.Detener();
        }
        else if(SceneManager.GetActiveScene().name == "Nivel1") 
        {
            Sonidos.controlSonidos.MusicaFondo(1);
        }
        else if(SceneManager.GetActiveScene().name == "Final")
        {
            Sonidos.controlSonidos.Detener();
            Sonidos.controlSonidos.MusicaFondo(0);
        }
    }
}

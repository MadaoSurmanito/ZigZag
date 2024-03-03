using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    public GameObject jugador;
    private GameObject suelo;
    
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el suelo que es el gameObject sobre el que actua el script
        suelo = GameObject.FindGameObjectWithTag("Lavaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Si jugador toca la lava, se reinicia el nivel
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Reinicia el nivel
            JugadorBola.velocidad = 6.0f;
            JugadorBola.lvl = 1;
            JugadorBola.distanciaSuelo = 6.0f;

            // carga SampleScene
            SceneManager.LoadScene("Inicio");
            
        }
    }
}

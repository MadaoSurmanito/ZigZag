using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float tiempo = 0;

    public Text textoTiempo;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Inicio")
        {
            tiempo = 0;
        }
        // Si la escena es la final, muestra el tiempo
        else if (SceneManager.GetActiveScene().name == "Final")
        {
            textoTiempo.text = "Has tardado: " + tiempo.ToString("f1") + " segundos";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si la escena no es la final, se actualiza el tiempo
        if (SceneManager.GetActiveScene().name != "Final" && SceneManager.GetActiveScene().name != "Inicio")
        {
            tiempo += Time.deltaTime;
            textoTiempo.text = "" + tiempo.ToString("f1");
        }
    }
}

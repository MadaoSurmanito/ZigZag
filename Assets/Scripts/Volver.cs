using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class Volver : MonoBehaviour
{
    // Public
    private Button boton;

    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindGameObjectWithTag("BotonFinal").GetComponent<Button>();
        boton.onClick.AddListener(cargar);
    }

    void cargar()
    {
        SceneManager.LoadScene("Inicio");
    }

    // Update is called once per frame
    void Update()
    {
    }
}

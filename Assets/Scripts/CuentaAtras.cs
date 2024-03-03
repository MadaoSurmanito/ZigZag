using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class CuentaAtras : MonoBehaviour
{
    // Public
    private Button boton;

    public Text textoContador;

    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindGameObjectWithTag("BotonEmpezar").GetComponent<Button>();
        boton.onClick.AddListener (Empezar);
    }

    void Empezar()
    {
        textoContador.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);
        StartCoroutine(cuentaAtras());
    }

    IEnumerator cuentaAtras()
    {
        for (int i = 0; i < 3; i++)
        {
            textoContador.text = (3 - i).ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
    // Update is called once per frame
    void Update()
    {
    }
}

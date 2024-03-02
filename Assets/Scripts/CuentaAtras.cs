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

    public Image imagen;

    public Sprite[] numeros;

    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindGameObjectWithTag("BotonEmpezar").GetComponent<Button>();
        boton.onClick.AddListener (Empezar);
    }

    void Empezar()
    {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);
        StartCoroutine(cuentaAtras());
    }

    IEnumerator cuentaAtras()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
    // Update is called once per frame
    void Update()
    {
    }
}

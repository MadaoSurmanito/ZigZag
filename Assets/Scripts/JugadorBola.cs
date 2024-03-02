using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorBola : MonoBehaviour
{
    // Public
    public Camera camara; // Referencia a la camara

    public GameObject suelo; // Referencia al suelo
 
    public float velocidad = 50.0f; // Velocidad de la bola

    // Private
    private Vector3 offset; // Offset de la camara

    private Vector3 DireccionActual; // Direccion actual de la bola

    private float ValX, ValZ; // Valores de X y Z para la creacion de suelos

    private int maxSuelos = 3; // Maximo de suelos que se pueden crear

    private int suelosCreados = 0; // Contador de suelos creados

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        offset = camara.transform.position; // Calcula el offset de la camara
        CrearSueloInicial(); // Crea el suelo inicial
        DireccionActual = Vector3.forward; // Inicializa la direccion de la bola
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Camara sigue a la bola
        camara.transform.position = transform.position + offset;
        // Si se pulsa la barra espaciadora, se cambia la direccion de la bola
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CambiarDireccion();
        }
        // Hacer que la bola se siga moviendo
        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
    }
    // Cambia la direccion de la bola
    void CambiarDireccion()
    {
        // Si la direccion actual es hacia adelante, cambia a la derecha
        if (DireccionActual == Vector3.forward)
        {
            DireccionActual = Vector3.right;
        }
        else // Si la direccion actual es hacia la derecha, cambia a la izquierda
        {
            DireccionActual = Vector3.forward;
        }
    }
    // Si jugador toca el suelo, se crea otro suelo
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            StartCoroutine(BorrarSuelos(other.gameObject));
        }
    }

    IEnumerator BorrarSuelos(GameObject suelo)
    {
        // debug contador de suelos
        Debug.Log("Suelos creados: " + suelosCreados);
        if (suelosCreados < maxSuelos)
        {
            float aleatorio = Random.Range(0.0f, 1.0f);
            if (aleatorio > 0.5)
            {
                ValX += 6.0f;
            }
            else
            {
                ValZ += 6.0f;
            }
            Instantiate(suelo,
            new Vector3(ValX, 15, ValZ),
            Quaternion.identity);
            suelosCreados++;
        }
        yield return new WaitForSeconds(1.5f);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1.5f);
        if (suelosCreados > 0)
        {
            Destroy (suelo);
            suelosCreados--;
        }
    }

    void CrearSueloInicial()
    {
        for (int i = 0; i < 3; i++)
        {
            ValZ += 6.0f;

            Instantiate(suelo,
            new Vector3(ValX, 15, ValZ),
            Quaternion.identity);
        }
    }
}

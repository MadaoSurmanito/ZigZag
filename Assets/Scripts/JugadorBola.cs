using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorBola : MonoBehaviour
{
    // Public
    public AudioClip sonidoDiamante;
    public AudioClip poyo;
    public Camera camara; // Referencia a la camara
    public GameObject suelo; // Referencia al suelo
    public float velocidad = 50.0f; // Velocidad de la bola
    // Private
    private Vector3 offset; // Offset de la camara
    private Vector3 DireccionActual; // Direccion actual de la bola
    private float ValX, ValZ; // Valores de X y Z para la creacion de suelos
    private int maxSuelos = 3; // Maximo de suelos que se pueden crear
    private int suelosCreados = 0; // Contador de suelos creados
    static int lvl = 1; // Nivel actual
    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        Sonidos.controlSonidos.Reproducir(poyo);
        offset = camara.transform.position; // Calcula el offset de la camara
        CrearSueloInicial(); // Crea el suelo inicial
        DireccionActual = Vector3.forward; // Inicializa la direccion de la bola
    }

    // Puntuacion del jugador
    private int puntuacion = 0;

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
        transform.Rotate(Vector3.forward, 100.0f, Space.World);
        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Aumenta la puntuacion si Kirby toca un premio
        if (other.gameObject.CompareTag("Premio"))
        {
            Sonidos.controlSonidos.Reproducir(sonidoDiamante);
            other.gameObject.SetActive(false);
            puntuacion = puntuacion + 1;
            Debug.Log("Puntuacion: " + puntuacion);
            // si la puntuacion es igual a 5, se cambia de nivel
            if (puntuacion == 5)
            {
                if(lvl == 4)
                {
                    SceneManager.LoadScene("Final");
                }
                lvl++;
                puntuacion = 0;
                // carga la escena de nivel 2
                SceneManager.LoadScene("Nivel"+lvl);
            }
        }
        other.gameObject.SetActive(false);
    }

    // Cambia la direccion de la bola
    void CambiarDireccion()
    {
        // Si la direccion actual es hacia adelante, cambia a la derecha
        if (DireccionActual == Vector3.forward)
        {
            DireccionActual = Vector3.right;
            //transform.Rotate(Vector3.right, 10.0f, Space.World);
        } // Si la direccion actual es hacia la derecha, cambia a la izquierda
        else
        {
            DireccionActual = Vector3.forward;
            //transform.Rotate(Vector3.forward, 10.0f, Space.World);
        }
    }

    // Si jugador toca el suelo, se crea otro suelo
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            StartCoroutine(GeneraSuelos(other.gameObject));
        }
    }

    IEnumerator GeneraSuelos(GameObject suelo)
    {
        // debug contador de suelos
        if (suelosCreados < maxSuelos)
        {
            SueloAleatoRio(suelo);
        }
        /* Hace que caiga un suelo cada 1,5 segundos */
        yield return new WaitForSeconds(1.5f);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1.5f);
        /* Hace que caiga un suelo cada 1,5 segundos */
        if (suelosCreados > 0)
        {
            Destroy (suelo);
            suelosCreados--;
        }
    }
    void SueloAleatoRio(GameObject suelo)
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
        GameObject newSuelo = Instantiate(suelo, new Vector3(ValX, 15, ValZ), Quaternion.identity);
        suelosCreados++;

        // Generar un premio aleatorio el cual es un prefab
        GameObject premio = GameObject.Find("Premio");
        float aleatorioPremio = Random.Range(0.0f, 1.0f);
        if (aleatorioPremio > 0.5)
        {
            Instantiate(premio, new Vector3(ValX, 16, ValZ), Quaternion.identity);
        }
    }
}

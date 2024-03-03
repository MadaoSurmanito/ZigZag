using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JugadorBola : MonoBehaviour
{
    // Public
    public AudioClip sonidoDiamante; // Sonido del diamante

    public AudioClip poyo; // Sonido al cambiar de nivel

    public Camera camara; // Referencia a la camara

    public GameObject suelo; // Referencia al suelo

    public Text textoPuntuacion; // Referencia al texto de la puntuacion

    // Private
    private Vector3 offset; // Offset de la camara

    private Vector3 DireccionActual; // Direccion actual de la bola

    private float

            ValX,
            ValZ; // Valores de X y Z para la creacion de suelos

    public int puntuacion = 0; // Puntuacion del jugador

    public static int lvl = 1; // Nivel actual

    public static float velocidad = 6.0f; // Velocidad de la bola
    public static float distanciaSuelo = 6.0f; // Distancia entre suelos
    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        Sonidos.controlSonidos.Reproducir (poyo);
        offset = camara.transform.position; // Calcula el offset de la camara
        CrearSueloInicial(); // Crea el suelo inicial
        DireccionActual = Vector3.forward; // Inicializa la direccion de la bola
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Si se pulsa la barra espaciadora, se cambia la direccion de la bola
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CambiarDireccion();
        }
    }

    // FixedUpdate se llama cada X tiempo, lo cual arregla que se cree el mapa de forma correcta
    void FixedUpdate()
    {
        // Hacer que la camara siga a la bola
        camara.transform.position = transform.position + offset;

        // Vector3 VelHorizontal = DireccionActual  * velocidad;
        // VelHorizontal.y = GetComponent<Rigidbody>().velocity.y;
        // GetComponent<Rigidbody>().velocity = VelHorizontal;
        // Hacer que la bola se siga moviendo
        //transform.Rotate(Vector3.forward, 100.0f, Space.World);
        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
    }

    void CrearSueloInicial()
    {
        for (int i = 0; i < 3; i++)
        {
            ValZ += distanciaSuelo;
            Instantiate(suelo,
            new Vector3(ValX, 15, ValZ),
            Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Aumenta la puntuacion si Kirby toca un premio
        if (other.gameObject.CompareTag("Premio"))
        {
            Sonidos.controlSonidos.Reproducir (sonidoDiamante);
            other.gameObject.SetActive(false);
            puntuacion = puntuacion + 1;
            textoPuntuacion.text = puntuacion + "/5";

            // si la puntuacion es igual a 5, se cambia de nivel
            if (puntuacion == 5)
            {
                lvl++;
                if (lvl >= 4)
                {
                    SceneManager.LoadScene("Final");
                }
                else
                {
                    puntuacion = 0;
                    velocidad += 4.0f;
                    if (lvl == 3)
                    {
                        distanciaSuelo = 4.0f;
                    }
                    // carga la escena de nivel siguiente
                    SceneManager.LoadScene("Nivel" + lvl);
                }
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
        SueloAleatorio (suelo);

        /* Hace que caiga un suelo cada 1,5 segundos */
        yield return new WaitForSeconds(1.5f);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1.5f);
        Destroy (suelo);
    }

    void SueloAleatorio(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        if (aleatorio > 0.5)
        {
            ValX += distanciaSuelo;
        }
        else
        {
            ValZ += distanciaSuelo;
        }
        GameObject newSuelo =
            Instantiate(suelo,
            new Vector3(ValX, 15, ValZ),
            Quaternion.identity);

        // Generar un premio aleatorio el cual es un prefab
        GameObject premio = GameObject.Find("Premio");
        float aleatorioPremio = Random.Range(0.0f, 1.0f);
        if (aleatorioPremio > 0.7)
        {
            Instantiate(premio,
            new Vector3(ValX, 16, ValZ),
            Quaternion.identity);
        }
    }
}

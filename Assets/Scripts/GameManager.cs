using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int puntuacion = 0;
    public int vidas = 3;
    public GameObject prefabPelota;
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoPuntuacion;

    // Evita que se procese la muerte varias veces antes de que se vuelva a generar una pelota
    private bool procesando = false;

    void Start()
    {
        ActualizarVida();
        ActualizarPuntos();
    }

    void Update()
    {
        // Si no hay pelotas en la escena y no se está procesando una muerte, entonces se pierde una vida
        if (!procesando && GameObject.FindGameObjectsWithTag("Pelota").Length == 0)
        {
            procesando = true;
            vidas--;
            ActualizarVida();

            // Si aún quedan vidas, genera una nueva pelota después de un breve retraso
            if (vidas > 0)
            {
                Invoke("SpawnPelota", 1);
            }
            // Si no quedan vidas, reinicia la escena
            else
            {
                Debug.Log("GAME OVER");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
       //si no quedan blocques termina la partida
        if (GameObject.FindGameObjectsWithTag("capa1").Length == 0 &&
            GameObject.FindGameObjectsWithTag("capa2").Length == 0 &&
            GameObject.FindGameObjectsWithTag("capa3").Length == 0 &&
            GameObject.FindGameObjectsWithTag("capa4").Length == 0 &&
            GameObject.FindGameObjectsWithTag("capa5").Length == 0 &&
            GameObject.FindGameObjectsWithTag("capa6").Length == 0)
        {
            Debug.Log("¡ESAAAAA ES,Has ganado!");
        }
    }

    // Genera una nueva pelota en el centro de la escena
    void SpawnPelota()
    {
        Instantiate(prefabPelota, Vector3.zero, Quaternion.identity);
        procesando = false; // Permet tornar a controlar quan torni a morir
    }

    // Actualiza el texto de vidas en la UI
    void ActualizarVida()
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;
    }
    public void ActualizarPuntos()
       {
        if (textoPuntuacion != null)
            textoPuntuacion.text = "Puntos: " + puntuacion;
    }

}


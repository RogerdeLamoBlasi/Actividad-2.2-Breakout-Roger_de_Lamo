using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int vidas = 3;
    public GameObject prefabPelota;
    public TextMeshProUGUI textoVidas;

    // Evita que se procese la muerte varias veces antes de que se vuelva a generar una pelota
    private bool procesando = false;

    void Start()
    {
        ActualizarTexto();
    }

    void Update()
    {
        // Si no hay pelotas en la escena y no se está procesando una muerte, entonces se pierde una vida
        if (!procesando && GameObject.FindGameObjectsWithTag("Pelota").Length == 0)
        {
            procesando = true;
            vidas--;
            ActualizarTexto();

            // Si aún quedan vidas, genera una nueva pelota después de un breve retraso
            if (vidas > 0)
            {
                Invoke("SpawnPelota", 1f);
            }
            // Si no quedan vidas, reinicia la escena
            else
            {
                Debug.Log("GAME OVER");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Genera una nueva pelota en el centro de la escena
    void SpawnPelota()
    {
        Instantiate(prefabPelota, Vector3.zero, Quaternion.identity);
        procesando = false; // Permet tornar a controlar quan torni a morir
    }

    // Actualiza el texto de vidas en la UI
    void ActualizarTexto()
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;
    }
}
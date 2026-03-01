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

    // Array de prefabs de power-ups para generar aleatoriamente
    public GameObject[] prefabsPowerUp;

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

    // Genera 3 pelotas en forma de abanico desde la posición de la pala
    public void SpawnEscopeta()
    {
        GameObject pala = GameObject.FindGameObjectWithTag("Pala");
        if (pala == null) return;

        Vector3 pos = pala.transform.position;

        // Offset para cada pelota: una a la izquierda, una al centro y una a la derecha
        float[] offsetsX = { -0.5f, 0f, 0.5f };

        for (int i = 0; i < 3; i++)
        {
            // Genera una nueva pelota con el prefab en la posición de la pala más el offset
            GameObject nuevaPelota = Instantiate(prefabPelota, pos, Quaternion.identity);
            Pelota pelotaScript = nuevaPelota.GetComponent<Pelota>();
            if (pelotaScript != null)
            {
                pelotaScript.gm = this;
                pelotaScript.Spawn();
            }
        }
    }

    // Multiplica el número de pelotas actuales, generando una nueva pelota por cada una existente
    public void MultiplicarPelotas()
    {
        GameObject[] pelotas = GameObject.FindGameObjectsWithTag("Pelota");
        int count = pelotas.Length;

        // Si no hay pelotas, no se hace nada
        for (int i = 0; i < count; i++)
        {
            GameObject p = pelotas[i];
            GameObject nuevaPelota = Instantiate(prefabPelota, p.transform.position, Quaternion.identity);
            Pelota pelotaScript = nuevaPelota.GetComponent<Pelota>();
            if (pelotaScript != null)
                pelotaScript.gm = this;

            // Genera una dirección aleatoria para la nueva pelota
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
            vidas += 1;
        }

        ActualizarVida();
    }

    // Genera una nueva pelota en el centro de la escena
    public void SpawnPelota()
    {
        Instantiate(prefabPelota, Vector3.zero, Quaternion.identity);
        procesando = false; 
    }

    // Actualiza el texto de vidas en la UI
    public void ActualizarVida()
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


using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float velocidad = 5f;
    private float velocidadX;
    private float velocidadY;

    public GameManager gm;

    void Start()
    {
        if (gm == null)
            gm = Object.FindFirstObjectByType<GameManager>();
        // Inicia la pelota en el centro
        Spawn(); 


    }
    void Update()
    {
        // Mueve la pelota cada frame según su velocidad
        transform.position += new Vector3(velocidadX, velocidadY, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Rebote contra paredes verticales (izquierda/derecha)
        if (collision.CompareTag("Vertical"))
        {
            velocidadX = -velocidadX;
        }

        // Rebote contra paredes Horizontales (arriba)
        if (collision.CompareTag("Horizontal"))
        {
            velocidadY = -velocidadY;
        }

        // Rebote contra Pala o Bloque
        if (collision.CompareTag("Pala") || collision.CompareTag("Block") || (collision.CompareTag("capa1") || collision.CompareTag("capa2") || collision.CompareTag("capa3") || collision.CompareTag("capa4") || collision.CompareTag("capa5") || collision.CompareTag("capa6")) ) {
            //Calcula la dirección de rebote basada en la posición relativa entre la pelota y el objeto colisionado
            float diffX = transform.position.x - collision.transform.position.x;
            float diffY = transform.position.y - collision.transform.position.y;
            float mag = Mathf.Sqrt(diffX * diffX + diffY * diffY);
            diffX /= mag;
            diffY /= mag;

            //Si la pelota está casi centrada respecto al objeto, rebota verticalmente.
            if (Mathf.Abs(diffX) < 0.3f)
            {
                velocidadX = 0;
                velocidadY = velocidad;
            }
            //Si la pelota está más a los lados, rebota en esa dirección.
            else
            {
                velocidadX = diffX * velocidad;
                velocidadY = diffY * velocidad;
            }

            // Si colisiona con un bloque, destruye el bloque y suma puntos
            if (collision.CompareTag("capa1") || collision.CompareTag("capa2") || collision.CompareTag("capa3") || collision.CompareTag("capa4") || collision.CompareTag("capa5") || collision.CompareTag("capa6"))
            {
                if (gm != null)
                {
                    switch (collision.tag)
                    {
                        case "capa1": gm.puntuacion += 1; break;
                        case "capa2": gm.puntuacion += 2; break;
                        case "capa3": gm.puntuacion += 3; break;
                        case "capa4": gm.puntuacion += 4; break;
                        case "capa5": gm.puntuacion += 5; break;
                        case "capa6": gm.puntuacion += 6; break;
                    }
                    gm.ActualizarPuntos();
                }
                Destroy(collision.gameObject);

                // 10% de probabilidad de soltar un power-up al destruir un bloque
                if (Random.value <= 0.1f && gm != null && gm.prefabsPowerUp.Length > 0)
                {
                    int index = Random.Range(0, gm.prefabsPowerUp.Length);
                    GameObject powerUpPrefab = gm.prefabsPowerUp[index];
                    GameObject nuevo = Instantiate(powerUpPrefab, collision.transform.position, Quaternion.identity);

                    // assignem el GM de manera explícita
                    PowerUp pu = nuevo.GetComponent<PowerUp>();
                    if (pu != null)
                    {
                        pu.tipo = (TipoPowerUp)index;
                        pu.gm = gm; 
                    }
                }
            }
        }

        // Si colisiona con la Zona de Muerte, destruye la pelota
        if (collision.CompareTag("ZonaMuerte"))
        {
            Destroy(gameObject);
        }
    }

    // Genera una nueva pelota en el centro con una dirección aleatoria
    public void Spawn()
    {
        transform.position = new Vector3(0, -2, 0);
        velocidadX = Random.Range(-1, 1);
        velocidadY = 1;
        Vector2 dir = new Vector2(velocidadX, velocidadY).normalized;
        velocidadX = dir.x * velocidad;
        velocidadY = dir.y * velocidad;
    }
}
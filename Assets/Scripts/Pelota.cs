using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float velocidad = 5f;
    private float velocidadX;
    private float velocidadY;

    void Start()
    {
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
        if (collision.CompareTag("Pala") || collision.CompareTag("Block"))
        {
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

            // Si colisiona con un bloque, destruye el bloque
            if (collision.CompareTag("Block"))
            {
                Destroy(collision.gameObject);
            }
        }

        // Si colisiona con la Zona de Muerte, destruye la pelota
        if (collision.CompareTag("ZonaMuerte"))
        {
            Destroy(gameObject);
        }
    }

    // Genera una nueva pelota en el centro con una dirección aleatoria
    void Spawn()
    {
        transform.position = Vector3.zero;
        velocidadX = Random.Range(-1f, 1f);
        velocidadY = 1f;
        Vector2 dir = new Vector2(velocidadX, velocidadY).normalized;
        velocidadX = dir.x * velocidad;
        velocidadY = dir.y * velocidad;
    }
}
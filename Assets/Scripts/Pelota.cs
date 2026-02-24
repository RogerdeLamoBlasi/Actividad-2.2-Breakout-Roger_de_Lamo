using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float velocidad = 5f;
    private float velocidadX;
    private float velocidadY;

    void Start()
    {
        Spawn();
    }
    void Update()
    {
        transform.position += new Vector3(velocidadX, velocidadY, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vertical"))
        {
            velocidadX = -velocidadX;
        }

        if (collision.CompareTag("Horizontal"))
        {
            velocidadY = -velocidadY;
        }

        if (collision.CompareTag("Pala") || collision.CompareTag("Block"))
        {
            float diffX = transform.position.x - collision.transform.position.x;
            float diffY = transform.position.y - collision.transform.position.y;
            float mag = Mathf.Sqrt(diffX * diffX + diffY * diffY);
            diffX /= mag;
            diffY /= mag;

            if (Mathf.Abs(diffX) < 0.3f)
            {
                velocidadX = 0;
                velocidadY = velocidad;
            }
            else
            {
                velocidadX = diffX * velocidad;
                velocidadY = diffY * velocidad;
            }
            if (collision.CompareTag("Block"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void Spawn()
    {
        transform.position = Vector3.zero;
        velocidadX = Random.Range(-1f, 1f) * velocidad;
        velocidadY = velocidad;
    }
}
using UnityEngine;

// Define los tipos de power-ups disponibles
public enum TipoPowerUp { BolaExtra, Escopeta, Multiplicar }

public class PowerUp : MonoBehaviour
{
    public TipoPowerUp tipo;
    public float velocidadCaida = 2;

    public GameManager gm;
    void Update()
    {
        // Hace que el power-up caiga lentamente hacia abajo
        transform.position += Vector3.down * velocidadCaida * Time.deltaTime;
    }

    // Al colisionar con la pala, se activa el efecto del power-up y se destruye el objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pala"))
        {
            if (gm != null)
            {
                switch (tipo)
                {
                    case TipoPowerUp.BolaExtra:
                        gm.SpawnPelota();   
                        gm.vidas += 1;
                        gm.ActualizarVida();
                        break;
                    case TipoPowerUp.Escopeta:
                        gm.SpawnEscopeta(); 
                        gm.vidas += 3;
                        gm.ActualizarVida();
                        break;
                    case TipoPowerUp.Multiplicar:
                        gm.MultiplicarPelotas();
                        break;
                }
            }
            Destroy(gameObject);
        }

        //se destruye al tocar el suelo
        if (collision.CompareTag("ZonaMuerte"))
            Destroy(gameObject);
    }
}
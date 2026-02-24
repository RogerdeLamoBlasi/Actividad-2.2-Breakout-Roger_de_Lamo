using UnityEngine;

public class controlPala : MonoBehaviour
{
    public float speed = 3f;
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * move * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vertical"))
        {
            Vector3 pos = transform.position;
            if (pos.x > 0) pos.x -= 0.1f;
            else pos.x += 0.1f;      
            transform.position = pos;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int Strong;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    [SerializeField] private AudioClip fireInpact;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void SetStrong(int s)
    {
        Strong = s;
    }

    public void DestroyMagic()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si la bala ha colisionado con un objeto que tiene el tag "Obstacle"
        if (gameObject.tag == "Projectile" && collision.gameObject.tag == "Enemy") return;

        if (gameObject.tag == "Magic" && collision.gameObject.tag == "Player") return;

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyScript>().TakeDamage(Strong);
                SoundsController.Instance.PlaySound(fireInpact);
            }
            
            if (gameObject.tag == "magic" && collision.gameObject.tag == "Obstacle")
            {
                SoundsController.Instance.PlaySound(fireInpact);
            }

            Destroy(gameObject);
        }
    }
}

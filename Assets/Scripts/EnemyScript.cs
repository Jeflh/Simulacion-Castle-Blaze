using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    
    [SerializeField] private Transform objetivo;
    public EnemysController enemysController;
    [SerializeField] private int Life;
    private float DeathDelay = 0.45f;

    private NavMeshAgent navMeshAgent;
    private Animator EnemyAnimator;
    private int Damage;

    public int currentRound = 0;

    private new Collider2D collider;
    [SerializeField] private AudioClip death;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        EnemyAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo == null) return;
        navMeshAgent.SetDestination(objetivo.position);

        if (enemysController.GetRound() % 2 == 0 && currentRound != enemysController.GetRound() ) // Reset de la UI de Vida
        {
            currentRound = enemysController.GetRound();
            for (int i = 0; i < currentRound; i++)
            {
                Life++;
            }
        }
    }

    public void TakeDamage(int strong)
    {
        Damage = 1 * strong;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Magic")
        {
            Life -= Damage;
            
            if (Life <= 0)
            {
                collider.enabled = false;
                Die();
            }
        }
    }

    void Die()
    {
        SoundsController.Instance.PlaySound(death);
        EnemyAnimator.SetTrigger("Die"); // reproducir la animación "Die"
        Invoke("DestroyGameObject", DeathDelay); // esperar "deathDelay" segundos antes de destruir el objeto
    }

    void DestroyGameObject()
    {
        enemysController.DecreaseEnemyCount();
        Destroy(gameObject);
    }
}

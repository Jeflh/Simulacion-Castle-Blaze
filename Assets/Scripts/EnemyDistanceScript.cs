using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilPrefab;
    [SerializeField] private Transform objetivo;
    [SerializeField] private int shootingDistance;
    private float LastShoot;

    [SerializeField] private AudioClip shoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaAlJugador <= shootingDistance && Time.time > LastShoot + 1.5f)
        {
            DispararProyectil();
            LastShoot = Time.time;
        }
    }

    void DispararProyectil()
    {   
        int MoveX, MoveY, rotation = 0;

        Vector3 direction = (objetivo.position - transform.position).normalized;

        MoveX = Mathf.RoundToInt(direction.x);
        MoveY = Mathf.RoundToInt(direction.y);

        if (MoveX == 1 && MoveY == 0) rotation = 0;

        else if (MoveX == -1 && MoveY == 0) rotation = 180;

        else if (MoveX == 0 && MoveY == 1) rotation = 90;

        else if (MoveX == 0 && MoveY == -1) rotation = -90;

        else if (MoveX == -1 && MoveY == 1) rotation = 140;

        else if (MoveX == 1 && MoveY == 1) rotation = 40;

        else if (MoveX == -1 && MoveY == -1) rotation = -140;

        else if (MoveX == 1 && MoveY == -1) rotation = -40;

        SoundsController.Instance.PlaySound(shoot);

        GameObject proyectil = Instantiate(projectilPrefab, transform.position + direction * 0.25f, Quaternion.Euler(0, 0, rotation));

        proyectil.GetComponent<MagicScript>().SetDirection(direction);
    } 
}

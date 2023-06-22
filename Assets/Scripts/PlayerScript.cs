using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int Life = 3;
    [SerializeField] private float Speed;
    [SerializeField] private int Strong;

    [SerializeField] private Joystick floatingJoystick;
    [SerializeField] private Joystick fixedJoystick;
    private Joystick joystick;

    [SerializeField] private EnemysController enemysController;
    [SerializeField] private GameObject MagicPrefab;
    [SerializeField] private GameObject[] Hearts;
    [SerializeField] private TimerScript timer;
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip levelUp;

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private DatabaseManager databaseManager;
    [SerializeField] private OptionsMenu optionsMenu;

    public event EventHandler deathPlayer;

    private Rigidbody2D PlayerRigidbody;
    private Animator PlayerAnimator;
    private Collider2D PlayerCollider2D;

    private float LastShoot;
    private int lastSide = -90;
    private Vector3 LastDirection = new Vector3(0f, -1f, 0);
    private float MoveX, MoveY;
    private Vector2 MoveInput;

    private int currentRound = 0;
    public int score = 0;
    public float timeElapsed;


    [SerializeField] private float invulnerabilityTime;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerCollider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Life < 2) Hearts[1].gameObject.SetActive(false);
        
        if (Life < 3) Hearts[2].gameObject.SetActive(false);
        
        if (Life < 4) Hearts[3].gameObject.SetActive(false);
        
        if (Life < 5) Hearts[4].gameObject.SetActive(false);
        
        if (Life < 6) Hearts[5].gameObject.SetActive(false);
        
        if (Life < 7) Hearts[6].gameObject.SetActive(false);
        
        if (Life < 8) Hearts[7].gameObject.SetActive(false);
        
        if (Life < 9) Hearts[8].gameObject.SetActive(false);
        
        if (Life < 10) Hearts[9].gameObject.SetActive(false);
        

        if (enemysController.GetRound() != currentRound) // Reset de la UI de Vida
        {
            // Establecer la invulnerabilidad del jugador
            isInvulnerable = true;

            // Después de un tiempo, el jugador ya no es invulnerable
            StartCoroutine(ResetInvulnerability(invulnerabilityTime + 0.5f));
            if (currentRound != 0) SoundsController.Instance.PlaySound(levelUp);

            if (enemysController.GetRound() % 3 == 0 && Speed < 6)
            {
                Speed++;
            }

            score += 10 * currentRound;

            currentRound = enemysController.GetRound();
            Strong++;

            Life = 0;

            for (int i = 0; i < currentRound + 2; i++)
            {
                Life++;
            }

            if (Life > 10) Life = 10;
            
            for (int i = 0; i < currentRound + 2; i++)
            {
                if (i == 10) break;
                Hearts[i].gameObject.SetActive(true);
            }
        }

        int joystickPreference = optionsMenu.LoadJoystickPreference();

        if (joystickPreference == 1)
        {
            fixedJoystick.gameObject.SetActive(true);
            joystick = fixedJoystick;
        }
        else
        {
            floatingJoystick.gameObject.SetActive(true);
            joystick = floatingJoystick;
        }

        MoveX = Mathf.Round(joystick.Horizontal);
        MoveY = Mathf.Round(joystick.Vertical);
        MoveInput = new Vector2(MoveX, MoveY).normalized;

        PlayerAnimator.SetFloat("Horizontal", MoveX);
        PlayerAnimator.SetFloat("Vertical", MoveY);
        PlayerAnimator.SetFloat("Speed", MoveInput.sqrMagnitude);

        // SOLO PARA PRUEBAS DESDE LA COMPUTADORA
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.50f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // Físicas
        PlayerRigidbody.MovePosition(PlayerRigidbody.position + MoveInput * Speed * Time.fixedDeltaTime);

        if(Speed > 0.001f)
        {
            if (MoveX == 1 && MoveY == 0)
            {
                lastSide = 0; //Derecha
                LastDirection = new Vector3(1f, 0f, 0f);
            }
            else if (MoveX == -1 && MoveY == 0)
            {
                lastSide = 180; // Izquierda
                LastDirection = new Vector3(-1f, 0f, 0f);
            }
            else if (MoveX == 0 && MoveY == 1)
            {
                lastSide = 90; // Arriba
                LastDirection = new Vector3(0f, 1f, 0f);
            }
            else if (MoveX == 0 && MoveY == -1)
            {
                lastSide = -90; // Abajo
                LastDirection = new Vector3(0f, -1f, 0f);
            }
            else if (MoveX == -1 && MoveY == 1)
            {
                lastSide = 140; // Arriba Izquierda
                LastDirection = new Vector3(-1f, 1f, 0f);
            }

            else if (MoveX == 1 && MoveY == 1)
            {
                lastSide = 40; // Arriba derecha
                LastDirection = new Vector3(1f, 1f, 0f);
            }
            else if (MoveX == -1 && MoveY == -1)
            {
                lastSide = -140; // Abajo Izquierda
                LastDirection = new Vector3(-1f, -1f, 0f);
            }
            else if (MoveX == 1 && MoveY == -1)
            {
                lastSide = -40; // Abajo derecha
                LastDirection = new Vector3(1f, -1f, 0f);
            }
            PlayerAnimator.SetFloat("Side", lastSide);
        }
    }

    public void ShootButton()
    {
        if (Time.time > LastShoot + 0.25)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = MoveInput;
        int rotation = 0;

        if (PlayerAnimator.GetFloat("Speed") < 0.001f)
        {
            direction = LastDirection.normalized;
            rotation = lastSide;
        }

        if (MoveX == 1 && MoveY == 0) rotation = 0;
        
        else if (MoveX == -1 && MoveY == 0) rotation = 180;
 
        else if (MoveX == 0 && MoveY == 1) rotation = 90;

        else if (MoveX == 0 && MoveY == -1) rotation = -90;

        else if (MoveX == -1 && MoveY == 1) rotation = 140;

        else if (MoveX == 1 && MoveY == 1) rotation = 40;
        
        else if (MoveX == -1 && MoveY == -1) rotation = -140;
        
        else if (MoveX == 1 && MoveY == -1) rotation = -40;

        SoundsController.Instance.PlaySound(shoot);
        GameObject Magic = Instantiate(MagicPrefab, transform.position + direction * 0.5f, Quaternion.Euler(0, 0, rotation));
        Magic.GetComponent<MagicScript>().SetStrong(Strong);
        Magic.GetComponent<MagicScript>().SetDirection(direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Projectile")
        {
            if (!isInvulnerable)
            {
                Life--;
                score -= 3;
                if (score < 0) score = 0;

                SoundsController.Instance.PlaySound(damage);

                if (Life <= 0)
                {
                    PlayerCollider2D.enabled = false;
                    timeElapsed = timer.StopTimer();
                    Hearts[0].gameObject.SetActive(false);
                    SoundsController.Instance.PlaySound(death);
                    deathPlayer?.Invoke(this, EventArgs.Empty);

                    string minutes = Mathf.Floor(timeElapsed / 60).ToString("00");
                    string seconds = (timeElapsed % 60).ToString("00");
                    string totalTime = minutes + ":" + seconds;

                    scoreManager.AddScore(score, currentRound, totalTime);

                    SaveScore();
     
                    Destroy(gameObject);
                }
                else
                {
                    // Establecer la invulnerabilidad del jugador
                    isInvulnerable = true;

                    // Después de un tiempo, el jugador ya no es invulnerable
                    StartCoroutine(ResetInvulnerability(invulnerabilityTime));
                }
            }
        }
    }
    private IEnumerator ResetInvulnerability(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInvulnerable = false;
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int s)
    {
        score += s;
    }

    public void SaveScore()
    {
        string name = PlayerPrefs.GetString("playerName", "Jugador");
        int topScore = scoreManager.GetTopScore();
        int topRound = scoreManager.GetTopRound();
        string topTime = scoreManager.GetTopTime();

        if (databaseManager.CheckNameExists(name) && databaseManager.CheckIfBestScore(name, topScore))
        {
           databaseManager.UpdateScore(name, topScore, topRound, topTime);
        }
        
        if (!databaseManager.CheckNameExists(name) && !databaseManager.CheckScoreExists(name, topScore, topRound, topTime))
        {
            databaseManager.InsertScore(name, topScore, topRound, topTime);

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region field configs
    [Header("Status")]
    [SerializeField] int health = 200;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    [Header("Projectile")]
    [SerializeField] float laserVelocity = 10f;
    [SerializeField] float firingPeriod = 0.1f;
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [SerializeField] AudioClip shootSoundEffect = null;

   
    #endregion

    Coroutine firedCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetupSingleton()
    {
        if(FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        Move();
        Fire();
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("FireLaser"))
        {

            firedCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("FireLaser"))
        {
            StopCoroutine(firedCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
               laserPrefab,
               transform.position,
               Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserVelocity);
            AudioSource.PlayClipAtPoint(shootSoundEffect, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(firingPeriod);
        }
        
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCam = Camera.main;

        xMin = gameCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - (padding + 7);

    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        DamageDealer damage = enemy.gameObject.GetComponent<DamageDealer>();
        if (!damage) return;
        PlayerHit(damage);
    }

    private void PlayerHit(DamageDealer damage)
    {
        health -= damage.GetDamage();
        damage.Hit();
        if (health <= 0) 
        { 
            health = 0;
            FindObjectOfType<Spaceship>().Die(gameObject);
            FindObjectOfType<LevelLoader>().LoadGameOver();
        }
    }

   
    public int GetHealth()
    {
        return health;
    }

    public void SetPlayerHealth(int health)
    {
        this.health = health;
    }

}

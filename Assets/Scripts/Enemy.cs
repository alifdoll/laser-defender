using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region field config
    [Header("Status")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile Info")]
    float shotCounter = 0f;
    [SerializeField] float minShotsInterval = 0.2f;
    [SerializeField] float maxShotsInterval = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectile = null;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [SerializeField] AudioClip shootSoundEffect = null;

    [Header("Death Effect")]
    [SerializeField] GameObject deathParticleEffects = null;
    [SerializeField] float explosionDuration = 1f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    #endregion

    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minShotsInterval, maxShotsInterval);
    }

    
    void Update()
    {
        ShootCountDown();
    }

    private void ShootCountDown()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minShotsInterval, maxShotsInterval);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
       AudioSource.PlayClipAtPoint(shootSoundEffect, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if (!damage) return;
        HitProcess(damage);
    }

    private void HitProcess(DamageDealer damage)
    {
        health -= damage.GetDamage();
        damage.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathParticleEffects, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}

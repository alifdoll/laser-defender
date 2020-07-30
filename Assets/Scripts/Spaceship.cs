using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
  
    [Header("Death Effect")]
    [SerializeField] GameObject deathParticleEffects = null;
    [SerializeField] float explosionDuration = 1f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    public void Die(GameObject gameObject)
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathParticleEffects, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}

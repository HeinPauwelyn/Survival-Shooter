using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public int currentBullets = 500;
    public int maxBullets = 1500;
    public Slider bulletSlider;
    public Text bulletCounter;
    public bool infinity = false;
    public AudioClip outOfAmmo;
    public AudioClip shoot;


    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        bulletSlider.maxValue = maxBullets;
        bulletSlider.value = currentBullets;

        if (infinity)
        {
            bulletCounter.text = "∞";
        }
        else
        {
            bulletCounter.text = currentBullets.ToString();
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            if (currentBullets != 0)
            {
                gunAudio.clip = shoot;
                Shoot();
            }
            else
            {
                gunAudio.clip = outOfAmmo;
            }
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void Refill()
    {
        if (currentBullets <= 0)
        {
            currentBullets = 500;
        }
        else
        {
            int newCount = currentBullets + 500;

            if (newCount > maxBullets)
            {
                currentBullets = maxBullets;
            }
            else
            {
                currentBullets = newCount;
            }
        }

        bulletSlider.value = currentBullets;
        bulletCounter.text = currentBullets.ToString();
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        currentBullets -= 1;
        bulletSlider.value = currentBullets;
        bulletCounter.text = currentBullets.ToString();
    }
}

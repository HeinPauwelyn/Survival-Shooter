using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Text healtCount;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        healthSlider.value = startingHealth;
        healtCount.text = startingHealth.ToString();
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
            ScoreManager.Score -= 5;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;
        healtCount.text = currentHealth.ToString();

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void GetDamage()
    {
        currentHealth = 100;
        healthSlider.value = currentHealth;
        healtCount.text = currentHealth.ToString();
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel(string command)
    {
        switch (command)
        {
            case "again":
                Application.LoadLevel(Application.loadedLevel);
                break;

            case "go_to_menu":
                Application.LoadLevel("Navigation");
                break;

            default:
                break;
        }

    }
}

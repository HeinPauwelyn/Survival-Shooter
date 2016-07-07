using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public Animator anim;

    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private bool isDead = false;
    private bool isSinking = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        ScoreManager.KilledEnemies += 1;
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }
    
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.Score += scoreValue;
        Destroy(gameObject, 2f);
    }
}

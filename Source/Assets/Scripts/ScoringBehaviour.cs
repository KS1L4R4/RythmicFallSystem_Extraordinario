using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class ScoringBehaviour : MonoBehaviour
{   
    [SerializeField] LayerMask destroyLayers;
    private BoxCollider box;
    private HazardsManager hazardsManager;
    private HealthManager healthManager;
    public TextMeshProUGUI scoreText;
    private RythmManager rythmManager;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject victoryUI;
    public int score;
    private float scoreMilestone;
    private float scoreModifyier;
    public bool isPaused;
    public bool isDead;
    public bool hasWon;
    public AudioSource basicAudioSource;
    public AudioSource advancedAudioSource;
    public AudioSource skippingAudioSource;
    public AudioSource shieldingAudioSource;
    public AudioSource healingAudioSource;
    public AudioSource damageAudioSource;
    public AudioSource buttonAudioSource;
    public AudioClip basicHazardClip;
    public AudioClip advancedHazardClip;
    public AudioClip skippingHazardClip;
    public AudioClip shieldingClip;
    public AudioClip damageClip;
    public AudioClip healingClip;
    public AudioClip buttonClip;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        rythmManager = FindAnyObjectByType<RythmManager>();
    }
    void Start()
    {
        HideDeathUI();
        HideVictoryhUI();
        healthManager = FindAnyObjectByType<HealthManager>();
        hazardsManager = FindAnyObjectByType<HazardsManager>();
        score = 0;
        scoreMilestone = 10;
        scoreModifyier = 1.6f;
        scoreText.text = "Score: " + score.ToString();
        pauseUI.SetActive(false);
        isPaused = false;
        isDead = false;
        hasWon = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPaused == false) //Destruye lo que se encuentre dentro del volumen de puntaje al pulsar una tecla
        {
            DestroyInside();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    void DestroyInside() //Destruye lo que se encuentre dentro del volumen de puntaje
    {
        Vector3 center = box.bounds.center;
        Vector3 halfExtents = box.bounds.extents;

        Collider[] hits = Physics.OverlapBox
        (
            center,
            halfExtents,
            transform.rotation,
            destroyLayers
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("BasicHazard")) //Condición respectiva del hazard básico
            {
                PlayBasicSFX();
                AddScore(1);
                Destroy(hit.gameObject);
            }
            if (hit.CompareTag("AdvancedHazard")) //Condición respectiva del hazard avanzado
            {
                PlayAvancedFX();
                AddScore(2);
                Destroy(hit.gameObject);
            }
            if (hit.CompareTag("SkippingHazard")) //Condición respectiva del hazard intermitente
            {
                PlaySkippingFX();
                AddScore(3);
                Destroy(hit.gameObject);
            }
            if (hit.CompareTag("ShieldingItem")) //Condición respectiva del item de escudo
            {
                AddScore(4);
                Destroy(hit.gameObject);
            }
            if (hit.CompareTag("HealingItem")) //Condición respectiva del item de curación
            {
                AddScore(5);
                Destroy(hit.gameObject);
            }
        }
    }
    public void AddScore(int ammountToAdd) //Agrega una cantidad por determinar al puntaje
    {
        score += ammountToAdd;
        scoreText.text = "Score: " + score.ToString();
        if(score >= scoreMilestone)
        {
            hazardsManager.UpdateSpeed();
            scoreMilestone = Mathf.CeilToInt(scoreMilestone * scoreModifyier);
        }
        if(score >= 100)
        {
            ShowVictoryUI();
        }
    }
    void TogglePause()
    {
        if(isDead == false)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            pauseUI.SetActive(isPaused);
        }
    }
    public void GoBackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
    public void ShowDeathUI()
    {
        isDead = true;
        Time.timeScale = 0;
        deathUI.SetActive(true);
    }
    public void ShowVictoryUI()
    {
        hasWon = true;
        Time.timeScale = 0;
        victoryUI.SetActive(true);
    }
    public void HideDeathUI()
    {
        isDead = false;
        Time.timeScale = 1;
        deathUI.SetActive(false);
    }
        public void HideVictoryhUI()
    {
        hasWon = false;
        Time.timeScale = 1;
        victoryUI.SetActive(false);
    }
    public void PlayBasicSFX()
    {
        basicAudioSource.PlayOneShot(basicHazardClip);
    }
    public void PlayAvancedFX()
    {
        advancedAudioSource.PlayOneShot(advancedHazardClip);
    }
    public void PlaySkippingFX()
    {
        skippingAudioSource.PlayOneShot(skippingHazardClip);
    }
    public void PlayShieldFX()
    {
        shieldingAudioSource.PlayOneShot(shieldingClip);
    }
    public void PlayDamageFX()
    {
        damageAudioSource.PlayOneShot(damageClip);
    }
    public void PlayHealingFX()
    {
        healingAudioSource.PlayOneShot(healingClip);
    }
    public void PlayPoppingFX()
    {
        buttonAudioSource.PlayOneShot(buttonClip);
    }
}
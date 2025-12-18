using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    private ScoringBehaviour scoringBehaviour;
    public bool shieldEnabled;
    public int health;
    public int shieldAmmount;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;

    void Start()
    {
        scoringBehaviour = FindAnyObjectByType<ScoringBehaviour>();
        health = 5;
        shieldAmmount = 0;
        shieldEnabled = false;
        healthText.text = "Health: " + health.ToString();
        shieldText.text = "Shield: " + shieldAmmount.ToString();
    }

    void Update()
    {
        if(health > 5)
        {
            health = 5;
        }
    }

    public void DecreaseHealth(int damage)
    {
        if (shieldEnabled == false)
        {
            health -= damage;
            if(health <= 0)
            {
                scoringBehaviour.ShowDeathUI();
            }
        }
        else if(shieldAmmount > 0)
        {
            shieldAmmount -= damage;
        }
        if(shieldAmmount <= 0)
        {
            shieldAmmount = 0;
            shieldEnabled = false;
        }
        shieldText.text = "Shield: " + shieldAmmount.ToString();
        healthText.text = "Health: " + health.ToString();
        scoringBehaviour.PlayDamageFX();
    }

    public void IncreaseHealth(int healing) //Logica ejecutada al curar al jugador
    {
        scoringBehaviour.PlayHealingFX();
        if(health < 5) //Si la vida es menor a la cantidad máxima
        {
            health += healing;
        }
        else //Si la salud está al máximo
        {
            scoringBehaviour.AddScore(5);
        }
        healthText.text = "Health: " + health.ToString(); //Actualiza la UI
    }

    public void ApplyShield() //Aplica escudo
    {
        scoringBehaviour.PlayShieldFX();
        shieldAmmount = 3;
        shieldEnabled = true;
        shieldText.text = "Shield: " + shieldAmmount.ToString();
    }
}

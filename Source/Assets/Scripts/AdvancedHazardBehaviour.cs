using UnityEngine;

public class AdvancedHazardBehaviour : MonoBehaviour
{
    private HealthManager healthManager;
    private HazardsManager hazardsManager;
    private ScoringBehaviour scoringBehaviour;
    private float advancedFalling;
    private float speedMultiplyier;

    void Awake()
    {
        int hazardLayer = LayerMask.NameToLayer("Hazards");
        Physics.IgnoreLayerCollision(hazardLayer, hazardLayer, true);
        healthManager = FindAnyObjectByType<HealthManager>();
        hazardsManager = FindAnyObjectByType<HazardsManager>();
        scoringBehaviour = FindAnyObjectByType<ScoringBehaviour>();
    }

    void Start()
    {
        speedMultiplyier = 1.2f;
        advancedFalling = hazardsManager.fallingSpeed * speedMultiplyier;
    }

    void Update()
    {
        transform.position += Vector3.down * advancedFalling * Time.deltaTime;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor")) //Lógica ejecutada al impactar con el suelo
        {
            if(healthManager.shieldEnabled == false) //Reduce la salud si no hay escudo activo
            {
                healthManager.DecreaseHealth(2);
                Destroy(gameObject);
            }
            else //Aumenta puntaje si hay escudo activo
            {
                scoringBehaviour.AddScore(2);
                healthManager.shieldAmmount -= 1;
                healthManager.DecreaseHealth(1);
                Destroy(gameObject);
            }
        }
    }
}

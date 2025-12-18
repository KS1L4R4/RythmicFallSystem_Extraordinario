using UnityEngine;

public class HealingItemBehaviour : MonoBehaviour
{
    private HealthManager healthManager;
    private HazardsManager hazardsManager;
    private ScoringBehaviour scoringBehaviour;
    private float speedMultiplyier;
    public float healingSpeed;

    void Awake() //Define la capa a ignorar, indica quela ignore, designa un healthManager y hazardsManager
    {
        int hazardLayer = LayerMask.NameToLayer("Hazards");
        Physics.IgnoreLayerCollision(hazardLayer, hazardLayer, true);
        healthManager = FindAnyObjectByType<HealthManager>();
        hazardsManager = FindAnyObjectByType<HazardsManager>();
        scoringBehaviour = FindAnyObjectByType<ScoringBehaviour>();
    }

    void Start()
    {
        speedMultiplyier = 1.5f;
        healingSpeed = hazardsManager.fallingSpeed * speedMultiplyier;
    }

    void Update() //Actualizar la posición del objeto
    {
        transform.position += Vector3.down * healingSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) //Destruirse y curar al tocar el suelo
    {
        if (collision.collider.CompareTag("Floor"))
        {
            if(healthManager.shieldEnabled == false)
            {
                healthManager.IncreaseHealth(1);
                Destroy(gameObject); 
            }
            else
            {
                healthManager.IncreaseHealth(2);
                scoringBehaviour.AddScore(2);
                Destroy(gameObject);
            }
        }        
    }
}

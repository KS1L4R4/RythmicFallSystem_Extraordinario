using UnityEngine;

public class ShieldingItemBehaviour : MonoBehaviour
{
    private HealthManager healthManager;
    private HazardsManager hazardsManager;
    private ScoringBehaviour scoringBehaviour;
    private float speedMultiplyier;
    public float shieldingSpeed;
    
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
        speedMultiplyier = 1.3f;
        shieldingSpeed = hazardsManager.fallingSpeed * speedMultiplyier;
    }

    void Update()
    {
        transform.position += Vector3.down * shieldingSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            if(healthManager.shieldEnabled == false)
            {
                healthManager.ApplyShield();
                Destroy(gameObject);
            }
            else
            {
                scoringBehaviour.AddScore(2);
                Destroy(gameObject);
            }
        }
    }
}

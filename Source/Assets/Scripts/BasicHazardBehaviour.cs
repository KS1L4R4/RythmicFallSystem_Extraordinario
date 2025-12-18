using UnityEngine;
using UnityEngine.UIElements;

public class BasicHazardBehaviour : MonoBehaviour
{
    private HealthManager healthManager;
    private HazardsManager hazardsManager;
    private ScoringBehaviour scoringBehaviour;
    public AudioSource audioSource;


    void Start()
    {
        int hazardLayer = LayerMask.NameToLayer("Hazards");
        Physics.IgnoreLayerCollision(hazardLayer, hazardLayer, true);
        healthManager = FindAnyObjectByType<HealthManager>();
        hazardsManager = FindAnyObjectByType<HazardsManager>();
        scoringBehaviour = FindAnyObjectByType<ScoringBehaviour>();
    }

    void Update()
    {
        transform.position += Vector3.down * hazardsManager.fallingSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            if(healthManager.shieldEnabled == false)
            {
                healthManager.DecreaseHealth(1);
                Destroy(gameObject);  
            }
            else
            {
                scoringBehaviour.AddScore(1);
                healthManager.shieldAmmount -= 1;
                healthManager.DecreaseHealth(1);
                Destroy(gameObject);
            }
        }
    }
}

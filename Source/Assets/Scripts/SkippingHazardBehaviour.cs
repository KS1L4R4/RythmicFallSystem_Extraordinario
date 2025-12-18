using System.Collections;
using UnityEngine;

public class SkippingHazardBehaviour : MonoBehaviour
{
    private HealthManager healthManager;
    private HazardsManager hazardsManager;
    private ScoringBehaviour scoringBehaviour;
    private float skippingSpeed;
    private float dropDistance;

    void Awake()
    {
        int hazardLayer = LayerMask.NameToLayer("Hazards");
        Physics.IgnoreLayerCollision(hazardLayer, hazardLayer, true);
        healthManager = FindAnyObjectByType<HealthManager>();
        scoringBehaviour = FindAnyObjectByType<ScoringBehaviour>();
        hazardsManager = FindAnyObjectByType<HazardsManager>();
    }

    void Start()
    {
        skippingSpeed = 0.5f;
        dropDistance = 2.5f;
        StartCoroutine(SkippingMovement());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            if(healthManager.shieldEnabled == false)
            {
                healthManager.DecreaseHealth(3);
                Destroy(gameObject);
            }
            else
            {
                scoringBehaviour.AddScore(3);
                healthManager.shieldAmmount -= 1;
                healthManager.DecreaseHealth(1);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SkippingMovement() //Corrutina para el movimiento intermitente del objeto
    {
        while (true)
        {
            transform.position += Vector3.down * dropDistance;
            yield return new WaitForSeconds(skippingSpeed);
        }
    }
}

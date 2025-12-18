using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class HazardsManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] hazards;
    public float fallingSpeed;
    public float speedModifyier;
    public int baseScoreSecondLevel;
    public int baseScoreThirdLevel;

    void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name; //Revisa cuál es el nombre de la escena que se abre
        if(sceneName == "TestLevel") //Define la velocidad de caida y puntajes para aumentar nivel al abrir el nivel
        {
            speedModifyier = 1.3f;
            fallingSpeed = 5;
            baseScoreSecondLevel = 20;
            baseScoreThirdLevel = 40;
        }
    }

    public void Spawn()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        int hazardIndex = UnityEngine.Random.Range(0, hazards.Length);

        Transform point = spawnPoints[index];
        Instantiate(hazards[hazardIndex], point.position, point.rotation);
        return;
    }

    public void UpdateSpeed()
    {
        fallingSpeed = Mathf.CeilToInt(fallingSpeed * speedModifyier);
    }
}
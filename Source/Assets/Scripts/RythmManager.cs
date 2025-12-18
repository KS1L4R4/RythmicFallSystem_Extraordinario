using System.Collections;
using UnityEngine;

public class RythmManager : MonoBehaviour
{
    public HazardsManager hazardsManager;
    public float rythm;
    public GameObject pauseUI;

    private void Start()
    {
        StartCoroutine(RythmicRead()); //Inicia corrutina que instancía los objetos cada cierto tiempo
    }
    void Update()
    {

    }

    IEnumerator RythmicRead() //Corrutina que instancía los objetos cada cierto tiempo
    {
        while (true)
        {
            hazardsManager.Spawn();
            yield return new WaitForSeconds(rythm);
        }
    }
}

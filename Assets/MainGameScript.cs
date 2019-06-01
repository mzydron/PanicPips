using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameScript : MonoBehaviour
{
    public GameObject Goal;
    public GameObject PipPrefab;
    
    public Vector2 sv = new Vector2(0, 0);
    public int maxPips = 300;
    
    int generation = 1;
    List<GameObject> pipList = new List<GameObject>();
    List<Vector2> bestMoves = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnGeneration", 0.5f, 8f);
        InvokeRepeating("KillThemPips", 8f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGeneration()
    {
        
        if (generation < 2)
        {
            PipPrefab.GetComponent<BestPip>().moveSet = null;
            for (int i = 0; i < maxPips; i++)
            {
                GameObject pip = Instantiate(PipPrefab, sv, Quaternion.identity);
                pipList.Add(pip);
            }
        }
        else
        {
            for (int i = 0; i < maxPips; i++)
            {
                EvolveMoveSet(PipPrefab.GetComponent<BestPip>().moveSet);
                GameObject pip = Instantiate(PipPrefab, sv, Quaternion.identity);
                pipList.Add(pip);
            }
        }

        generation += 1;
    }

    void KillThemPips()
    {
        GameObject[] ToExterminate = GameObject.FindGameObjectsWithTag("BestPip");
        ChampionPipElection();
        foreach (var pip in ToExterminate)
        {

                Destroy(pip);
        }
        pipList.Clear();
    }

    void ChampionPipElection()
    {
        float MaxFit = GameObject.FindGameObjectsWithTag("BestPip").Max(p => p.GetComponent<BestPip>().fitness);
        GameObject Champion = pipList.FirstOrDefault(p => p.GetComponent<BestPip>().fitness == MaxFit);
        bestMoves = Champion.GetComponent<BestPip>().moveSet;

        PipPrefab.GetComponent<BestPip>().moveSet = bestMoves;
    }

    void EvolveMoveSet(List<Vector2> originalMoveSet)
    {
        for (int i = 0;  i < originalMoveSet.Count; i++)
        {
            //float quartX = originalMoveSet[i].x / 0.25f;
            //float quartY = originalMoveSet[i].y / 0.25f;

            float quartX = 0.25f;
            float quartY = 0.25f;


            Vector2 change = new Vector2(Random.Range(-quartX, quartX), Random.Range(-quartY, quartY));
            
            originalMoveSet[i] += change;
        }
    }


}

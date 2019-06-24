using System;
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
    public int maxLifespan = 301;
    
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
        List<Vector2> oldGenMoveSetList = new List<Vector2>();
        List<GameObject> newPipList = new List<GameObject>();
        List<GameObject> best100 = new List<GameObject>();
        List<GameObject> newGen = new List<GameObject>();

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
            //for (int i = 0; i < maxPips; i++)
            //{
            //    //EvolveMoveSet(PipPrefab.GetComponent<BestPip>().moveSet);
            //    EvolveMoveSet(gameObject.GetComponent<MainGameScript>().bestMoves);
            //    GameObject pip = Instantiate(PipPrefab, sv, Quaternion.identity);
            //    pipList.Add(pip);
            //}0

            // Version 2

            //List<GameObject> futureGen = CreateNewGeneration();
            //foreach (GameObject newPip in futureGen)
            //{
            //    Instantiate(newPip, sv, Quaternion.identity);
            //    pipList.Add(newPip);
            //}


            // Version 3 



            //List<GameObject> thirdList = new List<GameObject>() ;
            //thirdList = pipList.OrderByDescending(p => p.GetComponent<BestPip>().fitness).ToList();
            //best100 = thirdList.Take(100).ToList();

            //newGen.Clear();

            //for (int i = 0; i < 100; i++)
            //{
            //    newGen.Add(thirdList[i]);
            //}


            //for (int i = 100; i < 600; i++)
            //{
            //    var tempPip = thirdList[i];

            //    EvolveMoveSet(tempPip.GetComponent<BestPip>().moveSet);

            //    newGen.Add(tempPip);
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    newGen.AddRange(best100);
            //}


            //foreach (var pip in newGen)
            //{
            //    // oldGenMoveSetList = pip.GetComponent<BestPip>().returnMoveSet();
            //    // List<Vector2> newMoveSet = (oldGenMoveSetList);

            //    GameObject newPip = Instantiate(PipPrefab, sv, Quaternion.identity);
            //    //EvolveMoveSet(newPip.GetComponent<BestPip>().moveSet = newMoveSet);
            //    newPipList.Add(newPip);
            //}
            //pipList = newPipList;


            // Version 4 

            

            

            foreach (var pip in pipList)
            {
                

                GameObject newPip = Instantiate(PipPrefab, sv, Quaternion.identity);
                EvolveMoveSet(newPip.GetComponent<BestPip>().returnMoveSet());
                //EvolveMoveSet(newPip.GetComponent<BestPip>().moveSet = newMoveSet);
                newPipList.Add(newPip);
            }


            pipList = newPipList;
        }
        generation += 1;
    }

    void KillThemPips()
    {
        
        foreach(GameObject pip in pipList)
        {
            pip.SetActive(false);
            
        }

        
    }

    private List<GameObject> CreateNewGeneration()
    {
        List<GameObject> newGen = new List<GameObject>();
        List<GameObject> best100fromPast = new List<GameObject>(pipList.OrderBy(p => p.GetComponent<BestPip>().fitness).ToList().GetRange(0,100));

        newGen.AddRange(best100fromPast);

        for (int i =0; i<5; i++)
        {
            foreach(GameObject pip in best100fromPast)
            {
                EvolveMoveSet(pip.GetComponent<BestPip>().moveSet);
            }
            newGen.AddRange(best100fromPast);
        }
        newGen.ForEach(p => p.GetComponent<BestPip>().lifespan = maxLifespan);
        //newGen.ForEach(p => p.GetComponent<BestPip>().transform.position = sv);
        newGen.ForEach(p => p.GetComponent<BestPip>().isDead = false);
        newGen.ForEach(p => p.GetComponent<BestPip>().gameObject.SetActive(true));
        
        pipList.Clear();
        return newGen;
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
            float quartX = 0.5f;
            float quartY = 0.5f;

            Vector2 change = new Vector2(UnityEngine.Random.Range(-quartX, quartX), UnityEngine.Random.Range(-quartY, quartY));
            originalMoveSet[i] += change;
        }
    }


}

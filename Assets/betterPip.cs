using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterPip : MonoBehaviour
{
    // Start is called before the first frame update

    bool isDead = false;
    public int lifespan = 200;
    float x, y;
    List<Vector2> moveSet = new List<Vector2>();
    
    void Start()
    {
        createMoveSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            lifespan = 0;
        }

        if (lifespan > 0)
        {
            Move(moveSet[lifespan]);
            lifespan -= 1;
            
            // Poor out of bands detection
        
            if (transform.position.x < GameObject.Find("LeftWall").transform.position.x 
                || transform.position.x > GameObject.Find("RightWall").transform.position.x)
            {
                isDead = true;
            }
            if (transform.position.y < GameObject.Find("DownWall").transform.position.y
                || transform.position.y > GameObject.Find("UpWall").transform.position.y)
            {
                isDead = true;
            }


            // Poor out of bands detection

            //if (transform.position.x < GameObject.Find("LeftWall").transform.position.x
            //    || transform.position.x > GameObject.Find("RightWall").transform.position.x)
            //{
            //    isDead = true;
            //}
            //if (transform.position.y < GameObject.Find("DownWall").transform.position.y
            //    || transform.position.y > GameObject.Find("UpWall").transform.position.y)
            //{
            //    isDead = true;
            //}
        }
    }


    void Move(Vector2 moveVector)
    {
        // Vector2 moveVector = new Vector2(x, y);
        Vector2 v = transform.position;
        transform.Translate(moveVector);
    }


    void createMoveSet()
    {
        for (int i = lifespan; i >= 0; i--)
        {
            x = Random.Range(-0.75f, 0.75f);
            y = Random.Range(-0.75f, 0.75f);

            moveSet.Add(new Vector2(x, y));
        }
    }

    
}

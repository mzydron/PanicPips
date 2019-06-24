using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestPip : MonoBehaviour
{

    public bool isDead = false;
    public float fitness;
    public int lifespan = 300;
    public GameObject PipPrefab;
    float x, y;
    Rigidbody2D rb;
    Vector2 maxspeed = new Vector2(3, 3);
    public List<Vector2> moveSet = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (moveSet.Count == 0)
        {
            createMoveSet();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            CalculateFitness();
        }
        else if (lifespan > 0 )
        {
            addSpeed(moveSet[lifespan]);
            lifespan -= 1;
        }
        else
        {
            isDead = true;
        }
    }
    
    void addSpeed(Vector2 v)
    {
        if (rb.velocity.x < maxspeed.x | rb.velocity.y < maxspeed.y)
        {
            rb.velocity += v;
        }
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

    public List<Vector2> returnMoveSet()
    {
        return this.moveSet;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isDead = true;
        if (collision.CompareTag("Goal"))
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);
        }
    }

    public void CalculateFitness()
    {
        float dist = Vector2.Distance(GameObject.FindGameObjectWithTag("Goal").transform.position, transform.position);
        fitness = 1 / (dist * dist);

    }

}

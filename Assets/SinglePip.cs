using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePip : MonoBehaviour
{
    public float x, y;
    public List<Vector2> ExemplaryMoveSet = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Move()
    {
        Vector2 moveVector = new Vector2( x, y);
        Vector2 v = transform.position;
        transform.Translate(moveVector);
    }



}

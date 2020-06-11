using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public float movementSpeed = 5f;

    private float h;
    private float v;

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
        h = h * movementSpeed * Time.deltaTime;
        v = v * movementSpeed * Time.deltaTime;

        transform.Translate(Vector3.right * h);
        transform.Translate(Vector3.left * v);
    }
}

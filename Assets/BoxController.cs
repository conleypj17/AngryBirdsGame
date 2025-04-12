using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; //package for action
public class BoxController : MonoBehaviour
{
    Vector3 direction;
    Rigidbody2D rb; //access rigid body from code
    Collider2D col;
    public Action onCatch; //“on..." for delegate variable

    //start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); //get to the current gameobject's rigidbody
        col = gameObject.GetComponent<Collider2D>();
    }

    //update is called once per frame
    void Update()
    {
        
    }
}

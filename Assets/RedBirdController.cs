using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement; //package for action

public class RedBirdController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float launchPower = 500f;
    Vector3 direction;
    Vector2 startPosition;  //starting position of the red bird
    Rigidbody2D rb; //access rigid body from code
    Collider2D col;
    Vector3 initialPosition;
    private bool birdWasLaunched;
    private float timeSittingAround;

    bool isDragging = false;

    public Action onCatch; //�on..." for delegate variable



    //start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>(); //get to the current gameobject's rigidbody
        col = gameObject.GetComponent<Collider2D>();
        rb.isKinematic = true; //this is so gravity doesn't pull it down while dragging
        startPosition = rb.position;
    }

    //updated is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, initialPosition);

        if (birdWasLaunched && GetComponent<Rigidbody2D>().linearVelocity.magnitude <= 0.1f)
        {
            timeSittingAround += Time.deltaTime;
        }

        gameObject.SetActive(true);
        if (isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // Keep it on the 2D plane
            rb.position = mouseWorldPos;
        }
        if (transform.position.y > 40 || transform.position.y < -40 || transform.position.x > 65 || transform.position.x < -20 || timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

    }
    void OnMouseDown()
    {
        isDragging = true;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
        Vector2 launchDirection = startPosition - rb.position;
        rb.AddForce(launchDirection * launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    public Vector2 GetLaunchDirection()
    {
        return (startPosition - rb.position).normalized;
    }

    public float GetLaunchPower()
    {
        return launchPower;
    }
}

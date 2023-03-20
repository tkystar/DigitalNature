using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.Find("Trail").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddForce(Vector3 launchVelocity)
    {
        rb.velocity = launchVelocity;
        transform.Find("Trail").gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    private ObjectPlacer objectPlacer;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.Find("Trail").gameObject.SetActive(false);
        
        // 生成クラス
        objectPlacer = GameObject.Find("ObjectPlacer").GetComponent<ObjectPlacer>();
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
        // 生成処理
        objectPlacer.PlaceObject(collision.contacts[0].point, - collision.contacts[0].normal);
        StartCoroutine(objectPlacer.PlayParticle(collision.contacts[0].point, - collision.contacts[0].normal));

        // 削除
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        transform.Find("Trail").gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionPosition
{
    Building,
    Ground
}

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    private ObjectPlacer objectPlacer;

    private CollisionPosition hitPosition;
    
    
    // debug
    private Vector3 pos;

    private Vector3 dir;
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
        Debug.DrawRay(pos, dir, Color.green);
        Debug.Log(pos + "," + dir);
    }

    public void AddForce(Vector3 launchVelocity)
    {
        rb.velocity = launchVelocity;
        transform.Find("Trail").gameObject.SetActive(true);
        gameObject.transform.parent = null;
        
        // 削除
        StartCoroutine(Destroy(10.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 生成処理
        if (collision.gameObject.tag == "Building")
        {
            objectPlacer.PlaceObject(collision.contacts[0].point, - collision.contacts[0].normal, CollisionPosition.Building);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            objectPlacer.PlaceObject(collision.contacts[0].point, - collision.contacts[0].normal, CollisionPosition.Ground);
        }
        else
        {
            return;
        }

        UnActive();
        StartCoroutine(objectPlacer.PlayParticle(collision.contacts[0].point, - collision.contacts[0].normal));
        pos = collision.contacts[0].point;
        dir = collision.contacts[0].normal;
        // 削除
        transform.Find("Trail").gameObject.SetActive(false);
        StartCoroutine(Destroy(2.0f));
    }

    private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // 一度接触した場合当たり判定をoffにする
    private void UnActive()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}

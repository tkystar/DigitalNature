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

    private BallRotater ballRotater;

    private CollisionPosition hitPosition;
    
    
    // debug
    private Vector3 pos;

    private Vector3 dir;

    private PinballGenerator pinballGenerator;
    
    private GameObject ballInstance;

    public bool isDragging = false;
    public Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    public Vector3 launchVelocity;

    //public Vector3 BallPosFromCam;

    public float force;

    private GUIStyle style;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.Find("Trail").gameObject.SetActive(false);

        
        // 生成クラス
        objectPlacer = GameObject.Find("ObjectPlacer").GetComponent<ObjectPlacer>();

        pinballGenerator = GameObject.Find("PinballController").GetComponent<PinballGenerator>();

        ballRotater = GameObject.Find("BallRotater").GetComponent<BallRotater>();
        
        // デバッグ用
        style = new GUIStyle();
        style.fontSize = 30;
    }

   
    // Update is called once per frame
    void Update()
    {
        if(!gameObject.CompareTag(ballRotater.SelectedBallType.ToString()))
        {
            return;
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<Collider>().Raycast(pinballGenerator.mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                isDragging = true;
                initialPosition = transform.position;
            }
        }
        
        if (isDragging)
        {
            //transform.localPosition = new Vector3(transform.localPosition.x,
             //   GetPlayerHitPoint().y, transform.localPosition.z);
             transform.position = new Vector3(transform.position.x,
                 GetPlayerHitPoint().y, transform.position.z);
            launchVelocity = pinballGenerator.mainCamera.transform.forward *
                             (transform.position - initialPosition).magnitude * force;

            //hitPointMarker.GetComponent<hitPointMarker>().SetPosition();


            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                dragEndPosition =
                    pinballGenerator.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                dragEndPosition.y = initialPosition.y;
                launchVelocity = pinballGenerator.mainCamera.transform.forward *
                                 (transform.position - initialPosition).magnitude * force;
                if (launchVelocity == Vector3.zero) return;

                // 発射させる
                AddForce(launchVelocity);
                gameObject.tag = "Untagged";
                StartCoroutine(pinballGenerator.SetBall(1.0f));
            }
        }
    }

    public void AddForce(Vector3 launchVelocity)
    {
        // 発射後はコリジョン判定をon 
        GetComponent<Collider>().isTrigger = false;
        
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
    
    private Vector3 GetPlayerHitPoint()
    {
        
        Ray ray = pinballGenerator.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            // 初期生成位置よりも上に行ったら(逆方向に引っ張ったら)
            if (hit.point.y > initialPosition.y)
            {
                return initialPosition;
            }
            //return hit.point;
            //return pinballGenerator.BallParent.transform.InverseTransformPoint(hit.point);
            return hit.point;
        }
        
        // 次のレイを発射するために、当たった位置から先を飛ばす
        ray = new Ray(hit.point + (ray.direction * 0.01f), ray.direction);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            // 初期生成位置よりも上に行ったら(逆方向に引っ張ったら)
            if (hit.point.y > initialPosition.y)
            {
                return initialPosition;
            }
            
            return hit.point;
            //return pinballGenerator.BallParent.transform.InverseTransformPoint(hit.point);

        }
        
        return transform.position;
    }
}

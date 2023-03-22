using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PinballController : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject ballInstance;
    
    
    public Camera mainCamera;

    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    public Vector3 launchVelocity;

    private Vector3 ballPosFromCam;

    public float force;

    public GameObject hitPointMarker;

    [SerializeField] private Transform instancePosition;

    [SerializeField] private Button instanceButton;
    private void Start()
    {
        ballPosFromCam = instancePosition.localPosition;
        //StartCoroutine(SetBall(0.1f));
        instanceButton.onClick.AddListener(() => { StartCoroutine(SetBall(0.1f)); });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ballInstance.GetComponent<Collider>().Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                isDragging = true;
                initialPosition = ballInstance.transform.position;
            }
        }

        if (isDragging)
        {
            ballInstance.transform.localPosition = new Vector3(ballInstance.transform.localPosition.x, GetPlayerHitPoint().y, ballInstance.transform.localPosition.z);
            launchVelocity = mainCamera.transform.forward * (ballInstance.transform.position - initialPosition).magnitude * force;

            //hitPointMarker.GetComponent<hitPointMarker>().SetPosition();
            
            
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                dragEndPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                dragEndPosition.y = initialPosition.y;
                launchVelocity = mainCamera.transform.forward * (ballInstance.transform.position - initialPosition).magnitude * force;
                if(launchVelocity == Vector3.zero) return;

                // 発射させる
                ballInstance.GetComponent<BallController>().AddForce(launchVelocity);
                StartCoroutine(SetBall(1.0f));
            }
        }
        
    }

    private IEnumerator SetBall(float time)
    {
        yield return new WaitForSeconds(time);
        ballInstance = Instantiate(ballPrefab, mainCamera.transform);
        ballInstance.transform.localPosition = ballPosFromCam;
        PlayScaleAnimation(ballInstance);

    }

    private void PlayScaleAnimation(GameObject scaledObj)
    {
        Vector3 originalScale = scaledObj.transform.localScale;
        scaledObj.transform.localScale = Vector3.zero;
        scaledObj.transform.DOScale(originalScale, 0.2f).SetEase(Ease.InCubic);
    }
    
    
    private Vector3 GetPlayerHitPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            // 初期生成位置よりも上に行ったら(逆方向に引っ張ったら)
            if (hit.point.y >= initialPosition.y)
            {
                return ballPosFromCam;
            }
            //return hit.point;
            return mainCamera.transform.InverseTransformPoint(hit.point);
        }
        
        // 次のレイを発射するために、当たった位置から先を飛ばす
        ray = new Ray(hit.point + (ray.direction * 0.01f), ray.direction);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            // 初期生成位置よりも上に行ったら(逆方向に引っ張ったら)
            if (hit.point.y >= initialPosition.y)
            {
                return ballPosFromCam;
            }
            
            //return hit.point;
            return mainCamera.transform.InverseTransformPoint(hit.point);

        }
        
        return ballPosFromCam;
    }
    
    
}

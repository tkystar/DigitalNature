using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows.WebCam;

public class PinballController : MonoBehaviour
{
    [FormerlySerializedAs("ball")] public GameObject ballPrefab;
    public Camera mainCamera;

    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    public Vector3 launchVelocity;

    private Vector3 ballPosFromCam;

    public float force;

    public GameObject hitPointMarker;

    private Rigidbody rb;

    private void Start()
    {
        ballPosFromCam = ballPrefab.transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ballPrefab.GetComponent<Collider>().Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                isDragging = true;
                initialPosition = ballPrefab.transform.position;
            }
        }

        if (isDragging)
        {
            ballPrefab.transform.position = new Vector3(ballPrefab.transform.position.x, GetPlayerHitPoint().y, ballPrefab.transform.position.z);
            launchVelocity = mainCamera.transform.forward * (ballPrefab.transform.position - initialPosition).magnitude * force;

            // 衝突地点の計算
            hitPointMarker.transform.position = CalculateCollisionPoint(ballPrefab.transform.position, launchVelocity,
                rb.mass,rb.drag,rb.angularDrag,Time.deltaTime);
            
            
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                dragEndPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                dragEndPosition.y = initialPosition.y;
                launchVelocity = mainCamera.transform.forward * (ballPrefab.transform.position - initialPosition).magnitude * force;
                Debug.Log("Force " + launchVelocity);
                ballPrefab.GetComponent<Rigidbody>().velocity = launchVelocity;
                //ball.GetComponent<Rigidbody>().useGravity = true;
                StartCoroutine(BallReset());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BallReset();
        }
    }

    private IEnumerator BallReset()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject ball = Instantiate(ballPrefab);
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.localPosition = ballPosFromCam;
        //ball.transform.position = new Vector3(ball.transform.position.x, initialPosition.y, ball.transform.position.z);
    }
    
    private Vector3 GetPlayerHitPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            return hit.point;
        }
        
        // 次のレイを発射するために、当たった位置から先を飛ばす
        ray = new Ray(hit.point + (ray.direction * 0.01f), ray.direction);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            return hit.point;
        }
        
        return Vector3.zero; // No player hit
    }
    
    public static Vector3 CalculateCollisionPoint(Vector3 initialPosition, Vector3 initialVelocity, float mass, float drag, float angularDrag, float deltaTime)
    {
        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        float time = 0f;
        float gravity = Physics.gravity.y;

        while (true)
        {
            // Update position using velocity and time
            position += velocity * deltaTime;

            // Update velocity using gravity and drag
            velocity += (gravity * Vector3.up + -drag * velocity) * deltaTime;

            // Calculate angular velocity using angular drag
            Vector3 angularVelocity = -angularDrag * velocity.normalized;

            // Check for collisions with other objects
            RaycastHit hit;
            if (Physics.Raycast(position, velocity.normalized, out hit, velocity.magnitude * deltaTime))
            {
                return hit.point;
            }

            // Increment time
            time += deltaTime;

            // Exit loop if object hits the ground
            if (position.y < 0f)
            {
                break;
            }
        }

        return Vector3.one * 100;
    }
}

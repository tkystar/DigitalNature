using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitPointMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition()
    {
        // 衝突地点の計算
        //transform.position = CalculateCollisionPoint(ballPrefab.transform.position, launchVelocity,
          //  rb.mass,rb.drag,rb.angularDrag,Time.deltaTime);
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

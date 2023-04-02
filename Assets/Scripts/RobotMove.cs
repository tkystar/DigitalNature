using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject startPosition;

    [SerializeField] private GameObject camera;

    private float distanceRobot;
    
    // Update is called once per frame
    private void Start()
    {
        
    }

    void Update()
    {
        Transform transform = this.transform;

        distanceRobot = Vector3.Distance(this.transform.localPosition, camera.transform.localPosition);
        
        transform.Translate(0, 0, speed);

        if (distanceRobot > 1000)
        {
            ResetRobotPosition();
        }
    }

    private void ResetRobotPosition()
    {
        float resetPositionX = camera.transform.localPosition.x;
        float resetPositionZ = camera.transform.localPosition.z;

        transform.position = new Vector3(resetPositionX, 300, resetPositionZ);
    }
}

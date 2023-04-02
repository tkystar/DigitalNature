using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject startPosition;

    [SerializeField] private GameObject rangeObjectMaxX;

    [SerializeField] private GameObject rangeObjectMiniX;

    [SerializeField] private GameObject rangeObjectMaxZ;

    [SerializeField] private GameObject rangeObjectMiniZ;

    [SerializeField] private GameObject Camera;

    [SerializeField] private GameObject robot;

    private float rangePositionMaxX;

    private float rangePositionMiniX;

    private float rangePositionMaxZ;

    private float rangePositionMiniZ;
    // Update is called once per frame
    private void Start()
    {
        rangePositionMaxX = rangeObjectMaxX.transform.localPosition.x;
        rangePositionMiniX = rangeObjectMiniX.transform.localPosition.x;
        rangePositionMaxZ = rangeObjectMaxZ.transform.localPosition.z;
        rangePositionMiniZ = rangeObjectMiniZ.transform.localPosition.z;
    }

    void Update()
    {
        Instantiate(robot);
        Transform transform = this.transform;

        transform.Translate(0, 0, speed);

        if (transform.localPosition.x > rangePositionMaxX || 
            transform.localPosition.x < rangePositionMiniX || 
            transform.localPosition.z > rangePositionMaxZ || 
            transform.localPosition.z < rangePositionMiniZ)
        {
            transform.localPosition = startPosition.transform.position;
        }
    }
}

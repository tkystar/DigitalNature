using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InstamtiateRobot : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject robot;

    [SerializeField] private float posY;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(InstantiateObject),3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateObject()
    {
        float instantiateRobotPositionX = camera.transform.localPosition.x;
        float instantiateRobotPositionZ = camera.transform.localPosition.z;
        Instantiate(robot, new Vector3(instantiateRobotPositionX, posY, instantiateRobotPositionZ), quaternion.identity);
    }
}

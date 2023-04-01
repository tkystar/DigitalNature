using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject startPosition;

    // Update is called once per frame
    void Update()
    {
        Transform transform = this.transform;

        transform.Translate(0, 0, speed);

        if (transform.localPosition.x > 550 || transform.localPosition.x < -420 || transform.localPosition.z > 620 || transform.localPosition.z < -560)
        {
            transform.localPosition = startPosition.transform.position;
        }
    }
}

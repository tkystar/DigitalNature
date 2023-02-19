using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject placedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 20, Color.red);
            if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, 20.0f))
            {
                Instantiate(placedObject, hit.point, Quaternion.identity);
            }
        }
    }
}

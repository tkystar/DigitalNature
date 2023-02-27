using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour
{
    // 置くオブジェクト
    [SerializeField] private GameObject placedObject;

    // オブジェクトを置くボタン
    [SerializeField] private Button shootButton;
    
    // Start is called before the first frame update
    void Start()
    {
        shootButton.onClick.AddListener(ObjectPlace);
    }

    private void FixedUpdate()
    {
       
    }


    private void ObjectPlace()
    {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 10000, Color.red);
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, Mathf.Infinity))
        {
            Instantiate(placedObject, hit.point, Quaternion.identity);
        }
    }
    
    
}

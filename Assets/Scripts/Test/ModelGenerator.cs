using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject prefab2;

    [SerializeField] private Quaternion quaternion;

    private GameObject instantiateModel;


    private Vector3 pos {
        get { return new Vector3(1, 1, 1); }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            instantiateModel = Instantiate(prefab, pos, quaternion);
            StartCoroutine(DeleteModel());
        }
        
        if(Input.GetMouseButtonDown(2))
        {
            instantiateModel = Instantiate(prefab2, pos, quaternion);
            StartCoroutine(DeleteModel());

        }
    }

    private IEnumerator DeleteModel()
    {
        yield return new WaitForSeconds(2);
        //Destroy(instantiateModel);
    }
}

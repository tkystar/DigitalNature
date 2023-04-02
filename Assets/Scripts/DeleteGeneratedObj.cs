using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGeneratedObj : MonoBehaviour
{
    [SerializeField] private GameObject parent;
   
    public void ClickedDeleteObj()
    {
        foreach (Transform child in parent.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}

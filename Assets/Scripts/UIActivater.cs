using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivater : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;

    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedActivateButton()
    {
        if (isActive)
        {
            uiCanvas.SetActive(false);
            isActive = false;
        }
        else
        {
            uiCanvas.SetActive(true);
            isActive = true;
        }
        
    }
}

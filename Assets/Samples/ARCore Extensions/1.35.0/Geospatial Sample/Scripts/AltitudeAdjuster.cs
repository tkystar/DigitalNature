using TMPro;
using UnityEngine.Serialization;

namespace Google.XR.ARCoreExtensions.Samples.Geospatial
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    public class AltitudeAdjuster : MonoBehaviour
    {
        private Button xUpButton;
        private Button xDownButton;
        private Button yUpButton;
        private Button yDownButton;
        private Button zUpButton;
        private Button zDownButton;

        public float moveUnit;


        private void Start()
        {
            yUpButton = GameObject.Find("yUpButton").GetComponent<Button>();
            yUpButton.onClick.AddListener(ClickedYUpButton);
            
            yDownButton = GameObject.Find("yDownButton").GetComponent<Button>();
            yDownButton.onClick.AddListener(ClickedYDownButton);
            
            xUpButton = GameObject.Find("xUpButton").GetComponent<Button>();
            xUpButton.onClick.AddListener(ClickedXUpButton);
            
            xDownButton = GameObject.Find("xDownButton").GetComponent<Button>();
            xDownButton.onClick.AddListener(ClickedXDownButton);
            
            //Z
            zUpButton = GameObject.Find("zUpButton").GetComponent<Button>();
            zUpButton.onClick.AddListener(ClickedZUpButton);
            
            zDownButton = GameObject.Find("zDownButton").GetComponent<Button>();
            zDownButton.onClick.AddListener(ClickedZDownButton);
        }

        private void ClickedYUpButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + moveUnit,
                transform.position.z);
        }

        private void ClickedYDownButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - moveUnit,
                transform.position.z);
        }
        
        private void ClickedXUpButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x + moveUnit, transform.position.y, transform.position.z);
        }

        private void ClickedXDownButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x - moveUnit, transform.position.y, transform.position.z);
        }
        
        private void ClickedZUpButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveUnit);
        }

        private void ClickedZDownButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveUnit);
        }
    }
}
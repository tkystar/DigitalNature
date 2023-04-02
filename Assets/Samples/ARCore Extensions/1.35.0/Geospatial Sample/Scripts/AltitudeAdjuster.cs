using TMPro;

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
        private Button valueUpButton;
        private Button valueDownButton;

        public float moveUnitY;


        private void Start()
        {
            valueUpButton = GameObject.Find("ValueUpButton").GetComponent<Button>();
            valueUpButton.onClick.AddListener(ClickedUpButton);
            
            valueDownButton = GameObject.Find("ValueDownButton").GetComponent<Button>();
            valueDownButton.onClick.AddListener(ClickedDownButton);
        }

        private void ClickedUpButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + moveUnitY,
                transform.position.z);
        }

        private void ClickedDownButton()
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - moveUnitY,
                transform.position.z);
        }
    }
}
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
        private TextMeshProUGUI debugLog;

        private Button changeValueButton;
        private TextMeshProUGUI altitudeValueText;

        private GeospatialController geospatialController;
        public AudioSource buttonpush;
        private ARAnchorManager AnchorManager;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DebugStatus());
        }

        private IEnumerator DebugStatus()
        {
            debugLog = GameObject.Find("debugLog").GetComponent<TextMeshProUGUI>();
            if (debugLog == null)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.1f);

            geospatialController = GameObject.Find("GeospatialController").GetComponent<GeospatialController>();

            if (geospatialController == null)
            {
                debugLog.text = "geospatialController is null.";
                yield break;
            }

            yield return new WaitForSeconds(0.1f);

            changeValueButton = GameObject.Find("ChangeValueButton").GetComponent<Button>();
            if (changeValueButton == null)
            {
                debugLog.text = "changeValueButton is null.";
                yield break;
            }

            changeValueButton.onClick.AddListener(ChangeValue);

            yield return new WaitForSeconds(0.1f);

            altitudeValueText = GameObject.Find("AltitudeValueText").GetComponent<TextMeshProUGUI>();
            if (altitudeValueText == null)
            {
                debugLog.text = "altitudeValueText is null.";
                yield break;
            }

            altitudeValueText.text = geospatialController.altitude.ToString();

            yield return new WaitForSeconds(0.1f);
            
            AnchorManager = GameObject.Find("AR Session Origin").GetComponent<ARAnchorManager>();
            if (AnchorManager == null)
            {
                debugLog.text = "AnchorManager is null.";
                yield break;
            }
            
            debugLog.text = "No Problem.";

        }

        private void ChangeValue()
        {
            buttonpush.Play();
            string value = altitudeValueText.text;
            if (value == null)
            {
                debugLog.text = "value is null.";
                return;
            }

            debugLog.text = value;
            debugLog.text = geospatialController.latitude.ToString();
            debugLog.text = geospatialController.longitude.ToString();
            debugLog.text = geospatialController.quaternion.ToString();

            var anchor = AnchorManager.AddAnchor(geospatialController.latitude, geospatialController.longitude, 300, geospatialController.quaternion);
            if (anchor == null)
            {
                debugLog.text = "anchor is null.";
                return;
            }
            gameObject.transform.position = anchor.transform.position;
            debugLog.text = anchor.ToString();
            

        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustAlpha : MonoBehaviour
{
    // 透明度を調整するスライダー
    [SerializeField] private Slider alphaAdjuster;

    // 透明度を調整する建物のマテリアル
    [SerializeField] private GameObject buildingModel;

    private Material buildingMaterial;
    // Start is called before the first frame update
    void Start()
    {
        buildingMaterial = buildingModel.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        buildingMaterial.color = new Color(1, 1, 1, alphaAdjuster.value);
    }

    private void GetAlphaValue(float sliderValue)
    {
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マテリアルをスイッチするモデル自体にアタッチする
/// トグルの名前が"SwitchMaterialToggle"になっていることを確認
/// </summary>
public class SwitchMaterial : MonoBehaviour
{
    // 切り替える2種類のマテリアル
    [SerializeField] private Material solidMaterial;
    [SerializeField] private Material alphaMaterial;

    // マテリアルの切り替え入力を受けるトグル
    private Toggle materialSwitchToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        materialSwitchToggle = GameObject.Find("SwitchMaterialToggle").GetComponent<Toggle>();
        materialSwitchToggle.onValueChanged.AddListener(ChangedValue);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
    }

    private void ChangedValue(bool isOn)
    {
        if (isOn)
        {
            gameObject.GetComponent<MeshRenderer>().material = alphaMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = solidMaterial;
        }
        
    }
}

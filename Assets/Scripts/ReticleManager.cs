using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ReticleManager : MonoBehaviour
{
    // レイキャストを補助するレティクル
    [SerializeField] private GameObject reticlePrefab;

    [SerializeField] private Transform rayOriginPosition;
    private GameObject reticle;
    
    private RaycastHit hit;
    private RaycastHit hitt;
    
    public string buildingTag = "Building"; // 対象のTag名
    public string groundTag = "Ground"; // 対象のTag名

    public float maxDistance = 1000.0f; // Rayが届く最大距離

    // Start is called before the first frame update
    void Start()
    {
        reticle = Instantiate(reticlePrefab);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        // Rayが対象のTagを持つオブジェクトに当たるまで飛ばす
        if (Physics.Raycast(rayOriginPosition.position,Camera.main.transform.forward,out hit, 1000f))
        {
            // 対象のTagを持つオブジェクトに当たった場合の処理
            if (hit.collider.CompareTag(buildingTag) || hit.collider.CompareTag(groundTag))
            {
                Activate();
                SetPosition(hit);
            }
            else
            {
                UnActivate();
            }
        }

    }

    private void Activate()
    {
        reticle.SetActive(true);
    }

    private void UnActivate()
    {
        reticle.SetActive(false);
    }

    private void SetPosition(RaycastHit hit)
    {
        // あたった位置に生成すると建物と被ってしまうため、法線ベクトル方向に少しずらす
        reticle.transform.position = hit.point + (hit.normal * 0.2f);
        reticle.transform.rotation = Quaternion.Slerp(reticle.transform.rotation, Quaternion.LookRotation(hit.normal), Time.deltaTime * 10.0f);
    }
}

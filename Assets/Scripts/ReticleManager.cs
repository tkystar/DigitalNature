using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    // レイキャストを補助するレティクル
    [SerializeField] private GameObject reticlePrefab;
    private GameObject reticle;
    
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        reticle = Instantiate(reticlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // レイがヒットしたら、ヒット地点にレティクルを表示させる
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, Mathf.Infinity))
        {
            Activate();
            SetPosition(hit);
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            return;
        }
        
        
        UnActivate();
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
        reticle.transform.position = hit.point + (hit.normal * 0.1f);
        reticle.transform.rotation = Quaternion.Slerp(reticle.transform.rotation, Quaternion.LookRotation(hit.normal), Time.deltaTime * 10.0f);
    }
}

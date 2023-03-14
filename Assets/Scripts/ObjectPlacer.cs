using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using DG.Tweening;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class ObjectPlacer : MonoBehaviour
{
    // 置くオブジェクト
    [SerializeField] private GameObject[] placedObject;

    // オブジェクトを置くボタン
    [SerializeField] private Button shootButton;
    
    // シュートボタンを押した時のAudioSorce
    [SerializeField] private AudioSource audio;
    
    // サウンドクリップ
    [SerializeField] private AudioClip pushedButton;
    [SerializeField] private AudioClip hitSE;
    
    // 生成パーティクル
    [SerializeField] private GameObject particlePrefab;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        shootButton.onClick.AddListener(ObjectPlace);
    }

    private void FixedUpdate()
    {
       
    }


    private void ObjectPlace()
    {
        
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 10000, Color.red);
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, Mathf.Infinity))
        {
            PlaySE(hitSE);
            StartCoroutine(PlayParticle(hit));
            GameObject instance = Instantiate(placedObject[Random.Range(0,placedObject.Length - 1)], hit.point, Quaternion.LookRotation(hit.normal));
            PlayScaleAnimation(instance);
        }
        else
        {
            PlaySE(pushedButton);
        }
    }

    private IEnumerator PlayParticle(RaycastHit hit)
    {
        GameObject particle = Instantiate(particlePrefab, hit.point, Quaternion.LookRotation(hit.normal));
        yield return new WaitForSeconds(1.0f);
        particle.transform.DOScale(Vector3.zero, 1.0f);
    }

    private void PlayScaleAnimation(GameObject scaledObj)
    {
        Vector3 originalScale = scaledObj.transform.localScale;
        scaledObj.transform.localScale = Vector3.zero;
        scaledObj.transform.DOScale(originalScale, 1f).SetEase(Ease.InCubic);
    }

    private void PlaySE(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
    
    
}

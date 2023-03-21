using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using DG.Tweening;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public enum ObjectType
{
    Plants,
    Neon
}
public class ObjectPlacer : MonoBehaviour
{
    // 置く植物オブジェクト
    [SerializeField] private GameObject[] buildingPlants;
    [SerializeField] private GameObject[] groundPlants;

    // 置くネオンオブジェクト
    [SerializeField] private GameObject[] placedNeon;
    
    // オブジェクトを置くボタン
    [SerializeField] private Button shootButton;
    
    // シュートボタンを押した時のAudioSorce
    [SerializeField] private AudioSource audio;
    
    // サウンドクリップ
    [SerializeField] private AudioClip hitSE;
    
    // 生成パーティクル
    [SerializeField] private GameObject particlePrefab;
    
    // 生成オブジェクトのタイプ選別トグル
    [SerializeField] private Toggle plantToggle;
    [SerializeField] private Toggle neonToggle;


    private ObjectType currentObjectType;
    
    // Start is called before the first frame update
    void Start()
    {
        if(shootButton != null)

            plantToggle.onValueChanged.AddListener((isOn) => { currentObjectType = ObjectType.Plants;});
        neonToggle.onValueChanged.AddListener((isOn) => { currentObjectType = ObjectType.Neon;});

    }

    private void FixedUpdate()
    {
       
    }
    

    public void PlaceObject(Vector3 position, Vector3 direction, CollisionPosition collisionPosition)
    {
        if (currentObjectType == ObjectType.Plants)
        {
            if (collisionPosition == CollisionPosition.Building)
            {
                GameObject instance = Instantiate(buildingPlants[Random.Range(0,buildingPlants.Length)], position, Quaternion.LookRotation(direction));
                PlayScaleAnimation(instance);
            }
            else if (collisionPosition == CollisionPosition.Ground)
            {
                GameObject instance = Instantiate(groundPlants[Random.Range(0,buildingPlants.Length)], position, Quaternion.LookRotation(direction));
                PlayScaleAnimation(instance);
            }
            
        }
        else if (currentObjectType == ObjectType.Neon)
        {
            GameObject instance = Instantiate(placedNeon[Random.Range(0,placedNeon.Length)], position, Quaternion.LookRotation(direction));
            PlayScaleAnimation(instance);

        }
        
        PlaySE(hitSE);
    }

    public IEnumerator PlayParticle(Vector3 position, Vector3 direction)
    {
        GameObject particle = Instantiate(particlePrefab, position, Quaternion.LookRotation(direction));
        yield return new WaitForSeconds(1.0f);
        particle.transform.DOScale(Vector3.zero, 1.0f);
    }

    private void PlayScaleAnimation(GameObject scaledObj)
    {
        Vector3 originalScale = scaledObj.transform.localScale;
        scaledObj.transform.localScale = Vector3.zero;
        scaledObj.transform.DOScale(Random.Range(0.8f, 1.2f) * originalScale, 1f).SetEase(Ease.InCubic);
    }

    private void PlaySE(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
    
    
}

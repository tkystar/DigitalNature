using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PinballGenerator : MonoBehaviour
{
    [HideInInspector] public GameObject selectedBallPrefab;
    private GameObject ballInstance;
    
    
    public Camera mainCamera;

    public bool isDragging = false;
    public Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    public Vector3 launchVelocity;
    

    public float force;
    
    [SerializeField] private Transform instancePosition;

    // 生成親オブジェクト
    public Transform BallParent;
    
    // セットしているボールの種類を持つクラス
    [SerializeField] private BallRotater BallRotater;
    
    
    // 植物ボールのプレハブ
    public GameObject PlantBallPrefab;
    
    // ネオンボールのプレハブ
    public GameObject NeonBallPrefab;

    private Vector3 plantsBallPos;
    private Vector3 neonBallPos;
    private Vector3 generatePos;
    
    
    private void Start()
    {
        plantsBallPos = instancePosition.localPosition;
        neonBallPos = new Vector3(plantsBallPos.x + 0.07f, plantsBallPos.y, plantsBallPos.z);

        Init();
    }

    private void Init()
    {
        // 生成
        GameObject plantBallInstance = Instantiate(PlantBallPrefab, BallParent);
        GameObject neonBallInstance = Instantiate(NeonBallPrefab, BallParent);

        // 位置を調整
        plantBallInstance.transform.localPosition = plantsBallPos;
        Debug.Log("plantsBallPos" + plantsBallPos);
        neonBallInstance.transform.localPosition = neonBallPos;

        // 初期選択ボールは植物に設定
        selectedBallPrefab = PlantBallPrefab;

    }
   

    public IEnumerator SetBall(float time)
    {
        yield return new WaitForSeconds(time);

        if (BallRotater.SelectedBallType == BallType.PlantBall)
        {
            selectedBallPrefab = PlantBallPrefab;
            generatePos = plantsBallPos;
        }
        else if (BallRotater.SelectedBallType == BallType.NeonBall)
        {
            selectedBallPrefab = NeonBallPrefab;
            generatePos = neonBallPos;
        }
        
        ballInstance = Instantiate(selectedBallPrefab, BallParent);
        ballInstance.transform.localPosition = generatePos;
        Debug.Log("generatePos" + generatePos);
        //PlayScaleAnimation(ballInstance);
    }

    private void PlayScaleAnimation(GameObject scaledObj)
    {
        Vector3 originalScale = scaledObj.transform.localScale;
        scaledObj.transform.localScale = Vector3.zero;
        scaledObj.transform.DOScale(originalScale, 0.2f).SetEase(Ease.InCubic);
    }
    
    
}

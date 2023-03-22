using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public enum BallType
{
    Plants,
    Neon,
    Signage
}
public class BallRotater : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [FormerlySerializedAs("redBall")] [SerializeField] private GameObject[] ball;

    private int currentSelectedBallNum;
    
    // 発射ボールの正常な大きさ
    private Vector3 originalScale;
    
    // ボールが左右に移動する単位
    public float moveUnit;

    [SerializeField] private ObjectPlacer objectPlacer;
    // Start is called before the first frame update
    void Start()
    {
        currentSelectedBallNum = 1;
        originalScale = ball[0].transform.localScale;
        InitializeScale();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < ball.Length; i++)
            {
                if (ball[i].transform.Find("model").GetComponent<Collider>().Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
                {
                    Rotate(i);
                    Debug.Log("i : " + i);
                }
            }
            
        }
    }

    private void InitializeScale()
    {
        // 選択されたボールのみサイズを拡大
        for (int i = 0; i < ball.Length; i++)
        {
            if (i == currentSelectedBallNum)
            {
                ball[i].transform.DOScale(originalScale, 0.3f);
            }
            else
            {
                ball[i].transform.DOScale(originalScale * 0.7f, 0.3f);
            }
        }
    }

    private void Rotate(int i)
    {
        if (i < currentSelectedBallNum)
        {
            for (int j = 0; j < ball.Length; j++)
            {
                ball[j].transform.DOLocalMoveX( ball[j].transform.localPosition.x + (moveUnit * Math.Abs( i - currentSelectedBallNum)), 0.3f);
                
                // 選択されたボールのみサイズを拡大
                if (j == i)
                {
                    ball[j].transform.DOScale(originalScale, 0.3f);
                }
                else
                {
                    ball[j].transform.DOScale(originalScale * 0.7f, 0.3f);

                }
            }
        }
        else
        {
            for (int j = 0; j < ball.Length; j++)
            {
                ball[j].transform.DOLocalMoveX(ball[j].transform.localPosition.x - (moveUnit * Math.Abs( i - currentSelectedBallNum)), 0.3f);
                
                // 選択されたボールのみサイズを拡大
                if (j == i)
                {
                    ball[j].transform.DOScale(originalScale, 0.3f);
                }
                else
                {
                    ball[j].transform.DOScale(originalScale * 0.7f, 0.3f);

                }
            }
        }

        Set(i);

    }


    private void Set(int i)
    {
        currentSelectedBallNum = i;
        if (i == 1)
        {
            objectPlacer.CurrentObjectType = ObjectType.Plants;
        }
        else if (i == 2)
        {
            objectPlacer.CurrentObjectType = ObjectType.Neon;
        }
        else if (i == 3)
        {
            objectPlacer.CurrentObjectType = ObjectType.Signage;
        }
    }
    
    
    
}

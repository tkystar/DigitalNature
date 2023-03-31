using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public enum BallType
{
    PlantBall,
    NeonBall,
    Signage
}
public class BallRotater : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    // ボールが左右に移動する単位
    public float moveUnit;

    [SerializeField] private ObjectPlacer objectPlacer;

    [SerializeField] private GameObject parentBall;

    public BallType SelectedBallType;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectedBallType = BallType.PlantBall;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedBallType == BallType.PlantBall)
            {
                if (GameObject.FindWithTag("NeonBall").GetComponent<Collider>().Raycast(
                        mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitNeonBall01, Mathf.Infinity))
                {
                    Slide(BallType.NeonBall);
                }
            }

            if (SelectedBallType == BallType.NeonBall)
            {
                if (GameObject.FindWithTag("PlantBall").GetComponent<Collider>().Raycast(
                        mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitPlantsBall01,
                        Mathf.Infinity))
                {
                    Slide(BallType.PlantBall);
                }
            }

        }
        
        /*
        if (Input.GetMouseButtonUp(0))
        {
            if (GameObject.FindWithTag("PlantBall").GetComponent<Collider>().Raycast(
                    mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitPlantsBall02, Mathf.Infinity))
            {
                SelectedBallType = BallType.PlantBall;

            }
            else if (GameObject.FindWithTag("NeonBall").GetComponent<Collider>().Raycast(
                         mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitNeonBall02, Mathf.Infinity))
            {
                SelectedBallType = BallType.NeonBall;

            }
        }
        */

    }

    
    

    private void Slide(BallType ballType)
    {
        if (ballType == BallType.PlantBall)
        {
            parentBall.transform.DOLocalMoveX( parentBall.transform.localPosition.x + moveUnit, 0.3f);
            StartCoroutine(Set(ballType));
        }
        else if (ballType == BallType.NeonBall)
        {
            parentBall.transform.DOLocalMoveX( parentBall.transform.localPosition.x - moveUnit, 0.3f);
            StartCoroutine(Set(ballType));

        }
        
        

    }


    private IEnumerator Set(BallType ballType)
    {
        if (ballType == BallType.PlantBall)
        {
            objectPlacer.CurrentObjectType = ObjectType.Plants;
        }
        else if (ballType == BallType.NeonBall)
        {
            objectPlacer.CurrentObjectType = ObjectType.Neon;
        }
        
        
        yield return new WaitForSeconds(0.1f);
        SelectedBallType = ballType;

    }
    
    
    
}

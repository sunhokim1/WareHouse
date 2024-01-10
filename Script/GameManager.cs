using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject roof;
    public static GameManager Instance; //static 전역 변수 
    public CinemachineVirtualCamera vCam1, vCam2, vCam3, vCam4;


    // Update is called once per frame
    void Update()
    {
        //고정 카메라 단축키 설정
        if (Input.GetKeyDown(KeyCode.Z))
        {
            vCam1.MoveToTopOfPrioritySubqueue();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            vCam2.MoveToTopOfPrioritySubqueue();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            vCam3.MoveToTopOfPrioritySubqueue();
        }
        //프리뷰 단축키
        if (Input.GetKeyDown(KeyCode.V))
        {
            vCam4.MoveToTopOfPrioritySubqueue();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            roof.SetActive(false); //지붕 숨기기
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            roof.SetActive(true); //지붕 보이기
        }

    }

}

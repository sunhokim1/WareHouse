using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject roof;
    public static GameManager Instance; //static ���� ���� 
    public CinemachineVirtualCamera vCam1, vCam2, vCam3, vCam4;


    // Update is called once per frame
    void Update()
    {
        //���� ī�޶� ����Ű ����
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
        //������ ����Ű
        if (Input.GetKeyDown(KeyCode.V))
        {
            vCam4.MoveToTopOfPrioritySubqueue();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            roof.SetActive(false); //���� �����
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            roof.SetActive(true); //���� ���̱�
        }

    }

}

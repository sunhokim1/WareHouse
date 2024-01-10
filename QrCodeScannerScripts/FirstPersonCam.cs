using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float turnSpeed = 4.0f; // 마우스 회전 속도
    public float moveSpeed = 2.0f; // 이동 속도
    public float zoomSpeed = 2.0f; // 줌 속도
    public float verticalSpeed = 2.0f; // 수직 이동 속도

    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )

    void Update()
    {
        MouseRotation();
        KeyboardMove();
        VerticalMove();
        Zoom();
    }

    // 마우스의 움직임에 따라 카메라를 회전 시킨다.
    void MouseRotation()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    // 키보드의 눌림에 따라 이동
    void KeyboardMove()
    {
        // WASD 키 또는 화살표키의 이동량을 측정
        Vector3 dir = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        // 이동방향 * 속도 * 프레임단위 시간을 곱해서 카메라의 트랜스폼을 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    // Q, E키를 눌러 카메라의 높이 조절
    void VerticalMove()
    {
        float verticalMove = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            verticalMove = -verticalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E)) 
        {
            verticalMove = verticalSpeed * Time.deltaTime;
        }
        transform.Translate(new Vector3(0, verticalMove, 0));
    }

    // 마우스 휠을 앞으로 이동하면 줌 인, 뒤로 이동하면 줌 아웃
    void Zoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scrollData * zoomSpeed);
    }
}

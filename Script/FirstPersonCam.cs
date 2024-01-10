using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float turnSpeed = 4.0f; // ���콺 ȸ�� �ӵ�
    public float moveSpeed = 2.0f; // �̵� �ӵ�
    public float zoomSpeed = 2.0f; // �� �ӵ�
    public float verticalSpeed = 2.0f; // ���� �̵� �ӵ�

    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    void Update()
    {
        MouseRotation();
        KeyboardMove();
        VerticalMove();
        Zoom();
    }

    // ���콺�� �����ӿ� ���� ī�޶� ȸ�� ��Ų��.
    void MouseRotation()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
        // Clamp �� ���� ������ �����ϴ� �Լ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    // Ű������ ������ ���� �̵�
    void KeyboardMove()
    {
        // WASD Ű �Ǵ� ȭ��ǥŰ�� �̵����� ����
        Vector3 dir = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        // �̵����� * �ӵ� * �����Ӵ��� �ð��� ���ؼ� ī�޶��� Ʈ�������� �̵�
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    // Q, EŰ�� ���� ī�޶��� ���� ����
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

    // ���콺 ���� ������ �̵��ϸ� �� ��, �ڷ� �̵��ϸ� �� �ƿ�
    void Zoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scrollData * zoomSpeed);
    }
}

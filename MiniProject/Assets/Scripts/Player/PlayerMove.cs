using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPos;
    private bool isMove = false;

    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        targetPos = transform.position;
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    private void Update()
    {
        TouchInput();
        MovePlayer();
        RestrictMovement();
    }

    private void TouchInput()
    {
        //��ġ �Է�
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z - transform.position.z));
                touchPos.y = transform.position.y;
                touchPos.z = transform.position.z;

                targetPos = touchPos;
                isMove = true;
            }
        }

        //���콺 �Է�(������)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - transform.position.z));
            mousePos.y = transform.position.y;
            mousePos.z = transform.position.z;

            targetPos = mousePos;
            isMove = true;
        }
    }

    private void MovePlayer()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                isMove = false;
            }
        }
    }

    private void RestrictMovement()
    {
        // ���� ��ġ�� ������
        Vector3 position = transform.position;

        // ȭ�� ��� ������ ��ġ�� ����
        position.x = Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x);
        position.y = Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y);

        // ���ѵ� ��ġ�� ����
        transform.position = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPos;
    private bool isMove = false;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        TouchInput();
        MovePlayer();
    }

    private void TouchInput()
    {
        //터치 입력
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

        //마우스 입력(디버깅용)
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
}

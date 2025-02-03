using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 targetPos;
    private bool isMove = false;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float spriteHalfWidth;
    private float spriteHalfHeight;

    private void Start()
    {
        targetPos = transform.position;
        mainCamera = Camera.main;

        // ȭ�� ��� ���
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));  // ���� �Ʒ�
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));  // ������ ��

        // ��������Ʈ ũ�� ��� (��������ŭ �߰�)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteHalfWidth = spriteRenderer.bounds.extents.x;  // ���� ���� ũ��
            spriteHalfHeight = spriteRenderer.bounds.extents.y; // ���� ���� ũ��
        }
    }

    private void Update()
    {
        TouchInput();
        MovePlayer();
        RestrictMovement();
    }

    private void TouchInput()
    {
        // ��ġ �Է� ó��
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                touchPos.y = transform.position.y;
                touchPos.z = transform.position.z;

                targetPos = touchPos;
                isMove = true;
            }
        }

        // ���콺 �Է� ó�� (������)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
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
        Vector3 position = transform.position;

        // ȭ�� ��踦 ����� �ʵ��� ��������Ʈ ũ�� ���
        position.x = Mathf.Clamp(position.x, minBounds.x + spriteHalfWidth, maxBounds.x - spriteHalfWidth);
        position.y = Mathf.Clamp(position.y, minBounds.y + spriteHalfHeight, maxBounds.y - spriteHalfHeight);

        transform.position = position;
    }
}

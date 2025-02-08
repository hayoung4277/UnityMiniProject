using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed;
    private Vector3 targetPos;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Vector2 touchOffset;

    private float spriteHalfWidth;
    private float spriteHalfHeight;

    private bool isSwiping = false;

    private Player player;

    private void Awake()
    {
        mainCamera = Camera.main;

        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();
    }

    private void Start()
    {
        targetPos = transform.position;

        speed = player.MoveSpeed;

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
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            touchWorldPos.z = transform.position.z; // 2D �����̹Ƿ� z ����

            if (touch.phase == TouchPhase.Began)
            {
                touchOffset = (Vector2)transform.position - (Vector2)touchWorldPos; // �ʱ� ��ġ ��ġ�� ĳ���� ��ġ ���� ����
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                targetPos = new Vector3(touchWorldPos.x + touchOffset.x, transform.position.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isSwiping = false;
            }
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    //private void TouchInput()
    //{
    //    if (Input.touchCount == 1)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
    //        touchWorldPos.z = transform.position.z; // 2D �����̹Ƿ� z�� ����

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            touchStartPos = touchWorldPos;
    //            touchOffset = (Vector2)transform.position - (Vector2)touchWorldPos; // �հ����� �÷��̾� ��ġ ���� ����
    //            isSwiping = true;
    //        }
    //        else if (touch.phase == TouchPhase.Moved && isSwiping)
    //        {
    //            transform.position = new Vector3(touchWorldPos.x + touchOffset.x, transform.position.y, transform.position.z);
    //        }
    //        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
    //        {
    //            isSwiping = false;
    //        }
    //    }
    //}


    private void RestrictMovement()
    {
        Vector3 position = transform.position;

        // ȭ�� ��踦 ����� �ʵ��� ��������Ʈ ũ�� ���
        position.x = Mathf.Clamp(position.x, minBounds.x + spriteHalfWidth, maxBounds.x - spriteHalfWidth);
        position.y = Mathf.Clamp(position.y, minBounds.y + spriteHalfHeight, maxBounds.y - spriteHalfHeight);

        transform.position = position;
    }
}

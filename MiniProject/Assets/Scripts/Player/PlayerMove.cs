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

        // 화면 경계 계산
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));  // 왼쪽 아래
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));  // 오른쪽 위

        // 스프라이트 크기 고려 (반지름만큼 추가)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteHalfWidth = spriteRenderer.bounds.extents.x;  // 가로 절반 크기
            spriteHalfHeight = spriteRenderer.bounds.extents.y; // 세로 절반 크기
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
            touchWorldPos.z = transform.position.z; // 2D 게임이므로 z 고정

            if (touch.phase == TouchPhase.Began)
            {
                touchOffset = (Vector2)transform.position - (Vector2)touchWorldPos; // 초기 터치 위치와 캐릭터 위치 차이 저장
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
    //        touchWorldPos.z = transform.position.z; // 2D 게임이므로 z값 고정

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            touchStartPos = touchWorldPos;
    //            touchOffset = (Vector2)transform.position - (Vector2)touchWorldPos; // 손가락과 플레이어 위치 차이 저장
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

        // 화면 경계를 벗어나지 않도록 스프라이트 크기 고려
        position.x = Mathf.Clamp(position.x, minBounds.x + spriteHalfWidth, maxBounds.x - spriteHalfWidth);
        position.y = Mathf.Clamp(position.y, minBounds.y + spriteHalfHeight, maxBounds.y - spriteHalfHeight);

        transform.position = position;
    }
}

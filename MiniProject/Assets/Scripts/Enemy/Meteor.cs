using System.Collections;
using UnityEngine;

public class Meteor : UnBreakable
{
    public string dataId = "050001";

    private Rigidbody2D rb;
    private Transform player;

    [Header("Warning & Line Prefabs")]
    public GameObject warningPrefab;  // 경고 이미지 프리팹
    public GameObject linePrefab;  // 경고 라인 프리팹

    private GameObject warningInstance;  // 생성된 경고 이미지
    private GameObject lineInstance;  // 생성된 경고 라인
    private Animator warningAnimator;  // 경고 애니메이터

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.UnBreakableTable.Get(dataId);

        var findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            player = findPlayer.transform;
        }

        IsSpawn = false;  // 스폰 전 상태

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"UnBreakable data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        StartCoroutine(WarningAndMoveCoroutine());  // 경고 -> 이동 코루틴 실행
    }

    private void Update()
    {
        if (!IsSpawn && player != null)
        {
            FollowPlayer();  // 등장 전 플레이어 위치 따라감
        }
    }

    // 플레이어의 x 좌표를 따라다니는 함수 (스폰 전)
    private void FollowPlayer()
    {
        Vector3 currentPos = transform.position;
        currentPos.x = player.position.x;  // x 좌표를 플레이어 위치로 고정
        transform.position = currentPos;

        if (lineInstance != null)
        {
            Vector3 linePosition = lineInstance.transform.position;
            linePosition.x = player.position.x;  // x 좌표만 플레이어에 맞추기
            lineInstance.transform.position = linePosition;
        }

        if(warningAnimator != null)
        {
            Vector3 warningPosition = warningInstance.transform.position;
            warningPosition.x = player.position.x;
            warningPosition.y = currentPos.y - 1f;
            warningInstance.transform.position = warningPosition;
        }

    }

    // 경고 -> 이동 코루틴
    private IEnumerator WarningAndMoveCoroutine()
    {
        // 경고 이미지 & 라인 생성
        warningInstance = Instantiate(warningPrefab, transform.position, Quaternion.identity);
        lineInstance = Instantiate(linePrefab, transform.position, Quaternion.identity);

        // 생성된 프리팹에서 Animator 가져오기
        warningAnimator = warningInstance.GetComponent<Animator>();

        if (warningAnimator != null)
        {
            warningAnimator.Play("WarningBlink");  // 애니메이션 실행
        }

        yield return new WaitForSeconds(1.2f);  // 애니메이션이 끝날 때까지 대기

        // 경고 이미지 & 라인 제거
        Destroy(warningInstance);

        IsSpawn = true;  // 스폰 상태 변경 (더 이상 플레이어 따라가지 않음)

        MoveUnBreakable(rb);  // 이동 시작
    }

    public override void MoveUnBreakable(Rigidbody2D rb)
    {
        base.MoveUnBreakable(rb);
    }

    public override void Destroyed()
    {
        base.Destroyed();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void Initialized(UnBreakableData data)
    {
        base.Initialized(data);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Destroy(lineInstance);
    }
}
using System.Collections;
using UnityEngine;

public class Meteor : UnBreakable
{
    public string dataId = "050001";

    private Rigidbody2D rb;
    private Transform player;

    [Header("Warning & Line Prefabs")]
    public GameObject warningPrefab;  // ��� �̹��� ������
    public GameObject linePrefab;  // ��� ���� ������

    private GameObject warningInstance;  // ������ ��� �̹���
    private GameObject lineInstance;  // ������ ��� ����
    private Animator warningAnimator;  // ��� �ִϸ�����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.UnBreakableTable.Get(dataId);

        var findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            player = findPlayer.transform;
        }

        IsSpawn = false;  // ���� �� ����

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
        StartCoroutine(WarningAndMoveCoroutine());  // ��� -> �̵� �ڷ�ƾ ����
    }

    private void Update()
    {
        if (!IsSpawn && player != null)
        {
            FollowPlayer();  // ���� �� �÷��̾� ��ġ ����
        }
    }

    // �÷��̾��� x ��ǥ�� ����ٴϴ� �Լ� (���� ��)
    private void FollowPlayer()
    {
        Vector3 currentPos = transform.position;
        currentPos.x = player.position.x;  // x ��ǥ�� �÷��̾� ��ġ�� ����
        transform.position = currentPos;

        if (lineInstance != null)
        {
            Vector3 linePosition = lineInstance.transform.position;
            linePosition.x = player.position.x;  // x ��ǥ�� �÷��̾ ���߱�
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

    // ��� -> �̵� �ڷ�ƾ
    private IEnumerator WarningAndMoveCoroutine()
    {
        // ��� �̹��� & ���� ����
        warningInstance = Instantiate(warningPrefab, transform.position, Quaternion.identity);
        lineInstance = Instantiate(linePrefab, transform.position, Quaternion.identity);

        // ������ �����տ��� Animator ��������
        warningAnimator = warningInstance.GetComponent<Animator>();

        if (warningAnimator != null)
        {
            warningAnimator.Play("WarningBlink");  // �ִϸ��̼� ����
        }

        yield return new WaitForSeconds(1.2f);  // �ִϸ��̼��� ���� ������ ���

        // ��� �̹��� & ���� ����
        Destroy(warningInstance);

        IsSpawn = true;  // ���� ���� ���� (�� �̻� �÷��̾� ������ ����)

        MoveUnBreakable(rb);  // �̵� ����
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
using UnityEngine;
using UnityEngine.UI;

public class GameStart : GenericUI
{
    public Button startButton;
    private AudioSource audioSource;

    private static Transform canvasTransform;

    private Transform child2;
    private Transform child3;
    private Transform child4;

    public Transform playerSpawnPoint;  // �߰�: �÷��̾� ���� ����Ʈ

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject canvasObj = GameObject.FindWithTag("Start");
        if (canvasObj != null)
        {
            canvasTransform = canvasObj.transform;
            child2 = canvasTransform.GetChild(1);
            child3 = canvasTransform.GetChild(2);
            child4 = canvasTransform.GetChild(3);

            // ó������ �÷��̾� ���� ȭ���� ��Ȱ��ȭ
            child4.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas�� ã�� �� �����ϴ�!");
        }
    }

    public override void Open()
    {
        base.Open();
        audioSource.Play();
    }

    // �÷��̾� ���� ȭ�� ����
    public void OpenPlayerSelect()
    {
        child2.gameObject.SetActive(false);
        child3.gameObject.SetActive(false);
        child4.gameObject.SetActive(true);
    }

    // �÷��̾� ���� ȭ�� �ݱ�
    public void ClosePlayerSelect()
    {
        child2.gameObject.SetActive(true);
        child3.gameObject.SetActive(true);
        child4.gameObject.SetActive(false);
    }

    public override void Close()
    {
        base.Close();
        audioSource.Stop();
    }

    // OnPlayerSelected�� ���� �޼��带 ���� ȣ������ �ʰ�, UIManager���� ó���ϵ��� �� �� �ֽ��ϴ�.
    public void OnPlayer1Selected()
    {
        // UIManager�� SpawnPlayer�� ȣ���Ͽ� �÷��̾� ����
        FindObjectOfType<UIManager>().OnPlayer1Selected();
    }

    public void OnPlayer2Selected()
    {
        // UIManager�� SpawnPlayer�� ȣ���Ͽ� �÷��̾� ����
        FindObjectOfType<UIManager>().OnPlayer2Selected();
    }

    public void OnPlayer3Selected()
    {
        // UIManager�� SpawnPlayer�� ȣ���Ͽ� �÷��̾� ����
        FindObjectOfType<UIManager>().OnPlayer3Selected();
    }
}
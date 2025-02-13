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

    public Transform playerSpawnPoint;  // 추가: 플레이어 스폰 포인트

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

            // 처음에는 플레이어 선택 화면을 비활성화
            child4.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas를 찾을 수 없습니다!");
        }
    }

    public override void Open()
    {
        base.Open();
        audioSource.Play();
    }

    // 플레이어 선택 화면 열기
    public void OpenPlayerSelect()
    {
        child2.gameObject.SetActive(false);
        child3.gameObject.SetActive(false);
        child4.gameObject.SetActive(true);
    }

    // 플레이어 선택 화면 닫기
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

    // OnPlayerSelected와 같은 메서드를 직접 호출하지 않고, UIManager에서 처리하도록 할 수 있습니다.
    public void OnPlayer1Selected()
    {
        // UIManager의 SpawnPlayer를 호출하여 플레이어 선택
        FindObjectOfType<UIManager>().OnPlayer1Selected();
    }

    public void OnPlayer2Selected()
    {
        // UIManager의 SpawnPlayer를 호출하여 플레이어 선택
        FindObjectOfType<UIManager>().OnPlayer2Selected();
    }

    public void OnPlayer3Selected()
    {
        // UIManager의 SpawnPlayer를 호출하여 플레이어 선택
        FindObjectOfType<UIManager>().OnPlayer3Selected();
    }
}
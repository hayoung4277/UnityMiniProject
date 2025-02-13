using System.Collections;
using System.Collections.Generic;
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

    private GameObject playerPrefab;
    private GameObject playerInstance;

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

    public void OpenPlayerSelect()
    {
        child2.gameObject.SetActive(false);
        child3.gameObject.SetActive(false);
        child4.gameObject.SetActive(true);
    }

    public void SpawnPlayer1()
    {
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player/MK01");
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }
        else if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = Instantiate(playerPrefab);
        }
    }

    public void SpawnPlayer2()
    {
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player/MK02");
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }
        else if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = Instantiate(playerPrefab);
        }


    }

    public void SpawnPlayer3()
    {
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player/MK03");
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }
        else if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = Instantiate(playerPrefab);
        }
    }

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
}

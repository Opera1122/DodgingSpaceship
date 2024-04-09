using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    static public GameManager instance;
    public GameObject timeText; //생존 시간 UI
    public GameObject bestTimeText;// 최고 기록 UI
    private float Survivetime; //생존 시간
    public bool isGameover; // 게임 오버 상태

    void Start()
    {
        bestTimeText.GetComponent<TextMeshProUGUI>().text = "Best Time : " + (int)PlayerPrefs.GetFloat("BestTime");
        instance = this;
        Survivetime = 0;
        isGameover = false;
    }

    void Update()
    {
        if (!isGameover)
        {
            //생존 시간 갱신
            Survivetime += Time.deltaTime;
            //갱신한 생존 시간 을 TimeText 텍스트로 컴포넌트를 이용해 표시
            timeText.GetComponent<TextMeshProUGUI>().text = "Time : " + (int)Survivetime;
        }
        else
        {
            End();
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void End()
    {
        //헌재 상태를 게임오버 상태로 변환
        isGameover = true;
        //게임오버 택스트게임 오브젝트를 활성화
        gameoverText.SetActive(true);
        //BestTime 키로 저장된 이전까지의 최고 기록 가져오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        
        if (Survivetime > bestTime)
        {
            //최고 기록 값을 현재 생존 시간 값으로 변경
            bestTime = Survivetime;

            //변경된 최고 기록을 BestTime 키로 저장
            PlayerPrefs.SetFloat("BestTime", bestTime);

        }
        //최고 기록을 bestTimeText 텍스트 컴포넌트를 이용해 표시
        bestTimeText.GetComponent<TextMeshProUGUI>().text = "Best Time : " + (int)bestTime;
    }
}


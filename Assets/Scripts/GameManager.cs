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
    public GameObject timeText; //���� �ð� UI
    public GameObject bestTimeText;// �ְ� ��� UI
    private float Survivetime; //���� �ð�
    public bool isGameover; // ���� ���� ����

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
            //���� �ð� ����
            Survivetime += Time.deltaTime;
            //������ ���� �ð� �� TimeText �ؽ�Ʈ�� ������Ʈ�� �̿��� ǥ��
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
        //���� ���¸� ���ӿ��� ���·� ��ȯ
        isGameover = true;
        //���ӿ��� �ý�Ʈ���� ������Ʈ�� Ȱ��ȭ
        gameoverText.SetActive(true);
        //BestTime Ű�� ����� ���������� �ְ� ��� ��������
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        
        if (Survivetime > bestTime)
        {
            //�ְ� ��� ���� ���� ���� �ð� ������ ����
            bestTime = Survivetime;

            //����� �ְ� ����� BestTime Ű�� ����
            PlayerPrefs.SetFloat("BestTime", bestTime);

        }
        //�ְ� ����� bestTimeText �ؽ�Ʈ ������Ʈ�� �̿��� ǥ��
        bestTimeText.GetComponent<TextMeshProUGUI>().text = "Best Time : " + (int)bestTime;
    }
}


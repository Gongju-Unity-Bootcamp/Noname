using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    public PlayerControler player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public GameObject RestartBtn;

    void Awake()
    {
        AudioManager.instance.PlayBgm(true);
    }

    public void Update()
    {
        player.Dead();
        UIPoint.text = (totalPoint + stagePoint).ToString();

        if (!player.islive) RestartBtn.SetActive(true);
    }

    public void NextStage()
    {
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
        }
        //GameOver
        else
        {
            Time.timeScale = 0;
            Debug.Log("Clear");
            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!!";
            RestartBtn.SetActive(true);

            AudioManager.instance.PlayBgm(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ending);

        }

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if(health > 0)
        {
            health--;
            UIhealth[health].color = new Color(1, 1, 1, 0.2f);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

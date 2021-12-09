using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;         //画面渐暗的持续时间
    public float displayImageDuration = 5f;         //结束画面展示的持续时间
    public GameObject player;           //对游戏对象的引用
    public CanvasGroup exitBackgroundImageCanvasGroup;          //对获胜图像的引用
    public AudioSource exitAudio;           //对获胜音频的引用
    public CanvasGroup caughtBackgroundImageCanvasGroup;            //对失败图像的引用
    public AudioSource caughtAudio;         //对失败音频的引用
    public MovingAudioControl movingAudioControl;           //对移动音频控制器的引用
    public SprintingAudioContro sprintingAudioControl;          //对奔跑音频控制器的引用

    bool m_IsPlayerAtExit;          //判断玩家是否到达出口的变量
    bool m_IsPlayerCaught = false;          //判断玩家是否失败的变量
    float m_Timer;          //计时变量
    bool m_HasAudioPlayed;          //判断音频是否播放的变量




    // Start is called before the first frame update
    void Start()
    {
        
    }

    //若玩家到达了出口，将判断玩家是否到达出口的变量设置为true
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit)           //若玩家到达了出口，显示获胜图片，不进入小黑屋，播放获胜音频
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)          //若玩家失败了，显示失败图片，进入小黑屋，播放失败音频
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }



    void EndLevel(CanvasGroup imageCanvasGroup, bool darkRoom, AudioSource audioSource)
    {
        //停止当前正在播放的走路和跑步音效
        AudioSource as1 = movingAudioControl.GetComponent<AudioSource>();
        as1.Stop();
        AudioSource as2 = sprintingAudioControl.GetComponent<AudioSource>();
        as2.Stop();

        //禁用走路和跑步音频控制脚本
        movingAudioControl.GetComponent<MovingAudioControl>().enabled = false;
        sprintingAudioControl.GetComponent<SprintingAudioContro>().enabled = false;


        if (!m_HasAudioPlayed)          //若结束音频未播放
        {
            audioSource.Play();         //播放结束音频
            m_HasAudioPlayed = true;            //将flag设置为true，防止每次Update都播放一次音频
        }


        m_Timer += Time.deltaTime;          //计时变量增加

        imageCanvasGroup.alpha = m_Timer / fadeDuration;            //修改结束图片的alpha，当计时变量大于fadeDuration时alpha=1，图片完全显现出来

        if (m_Timer > fadeDuration + displayImageDuration)          //当显示时间结束时
        {
            if (darkRoom)           //如果要进入小黑屋，调用SceneManager切换至小黑屋场景
            {
                SceneManager.LoadScene(2);
                Debug.Log("LoadScene(2)");
            }
       
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
        
    }

    public bool IsWin()
    {
        return m_IsPlayerAtExit;
    }
}

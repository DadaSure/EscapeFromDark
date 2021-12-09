using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingAudioContro : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] sprintingAudio;            //引用奔跑音效的数组
    float timerSound = 0f;                           //音频播放计时变量
    bool isSprinting;           //判断玩家是否奔跑的变量
    int sprintingAudioIndex = 0;            //当前播放的奔跑音效索引
    bool isSprintingAudioPlaying = false;           //判断当前奔跑音效是否在播放的变量

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //增加音频播放计时器，当比较大时重置一下
        timerSound += Time.deltaTime;
        if (timerSound > 1000)
        {
            timerSound = 0f;
        }

        //如果在奔跑的话
        if (isSprinting)
        {
            if (!isSprintingAudioPlaying)           //现在奔跑音频没有播放
            {
                // 通过对音频计时变量强制类型转换为int，与奔跑音频数组长度取余的方法实现随机指定一个新的音频索引
                sprintingAudioIndex = (int)timerSound % sprintingAudio.Length;
                audioSource.clip = sprintingAudio[sprintingAudioIndex];
                audioSource.Play();         //播放音频
                isSprintingAudioPlaying = true;         //将flag设置为true，直到flag为false的时候才会再进入这个判断分支指定一个新的索引来播放
            }
        }

        //如果指定的奔跑音频没有在播放，将flag设置为false以便在下次Update播放新的音频
        if (!audioSource.isPlaying)
        {
            isSprintingAudioPlaying = false;
        }


        //检测是否按下LeftShift键进行奔跑
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }


    }
}

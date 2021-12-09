using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAudioControl : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] manAudio;            //引用大汉音效的数组
    int manAudioIndex = 0;            //当前播放的大汉音效索引
    bool isManAudioPlaying = false;           //判断当前大汉音效是否在播放的变量
    public PlayerControl playerControl;         //引用玩家控制类
    public GameEnding gameEnding;           //引用游戏结束类

    bool playManAudio = true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControl.currentHealth <= 0 || gameEnding.IsWin())
        {
            playManAudio = false;
        }

        if (playManAudio)
        {
            if (!isManAudioPlaying)           //现在大汉音频没有播放
            {
                //循环指定新的大汉音效来播放
                manAudioIndex += 1;
                if (manAudioIndex > manAudio.Length)
                {
                    manAudioIndex = 0;
                }

                audioSource.clip = manAudio[manAudioIndex];
                audioSource.Play();         //播放音频
                isManAudioPlaying = true;         //将flag设置为true，直到flag为false的时候才会再进入这个判断分支指定一个新的索引来播放
            }
        }


        //如果指定的大汉音频没有在播放，将flag设置为false以便在下次Update播放新的音频
        if (!audioSource.isPlaying)
        {
            isManAudioPlaying = false;
        }
    }


}

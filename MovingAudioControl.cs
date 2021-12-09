using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAudioControl : MonoBehaviour
{
    AudioSource audioSource;            //引用AudioSource
    bool isMoving;          //用于判断是否在移动的flag

    public AudioClip walkAudio;
    public AudioClip sprintAudio;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //检测是否按下WASD键进行移动
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //检测是否按下LeftShift进行奔跑，切换走路和跑步音频
        if (Input.GetKey(KeyCode.LeftShift))
        {
            audioSource.clip = sprintAudio;
        }
        else
        {
            audioSource.clip = walkAudio;
        }


        if (isMoving)           //若在移动
        {
            if (!audioSource.isPlaying)         //且当前移动没有在播放
            {
                audioSource.Play();         //播放移动音效
            }
        }
        else
        {
            audioSource.Stop();         //没有在移动的话就把现在放的音效停下来
        }
    }
}

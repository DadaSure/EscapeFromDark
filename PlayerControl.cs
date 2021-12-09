using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    AudioSource audioSource;            //引用AudioSource

    public int startingHealth = 100;                            // 玩家初始生命值
    public int currentHealth;                                   // 玩家当前生命值
    public Image damageImage;                                   // 引用受伤UI图片
    public float flashSpeed = 5f;                               // 受伤UI图片播放时间
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // 受伤UI效果颜色
    public AudioClip[] hurtAudio;                                    // 引用受伤音效的数组


    bool damaged;                                               // 判断玩家是否受伤的变量
    float timerSound = 0f;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;         //初始化生命值
        audioSource = GetComponent<AudioSource>();            //获取AudioSource组件
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


        // 如果玩家受伤
        if (damaged)
        {
            // 将受伤UI图片颜色改为效果颜色
            damageImage.color = flashColour;
        }
        // 否则
        else
        {
            // 渐变为空白颜色
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // 将受伤flag重置
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        // 将受伤flag设置为true
        damaged = true;

        // 减少玩家的生命值
        currentHealth -= amount;

        // 通过对音频计时变量强制类型转换为int，与受伤音频数组长度取余的方法实现随机指定音频播放
        int n = (int)timerSound % hurtAudio.Length;
        audioSource.clip = hurtAudio[n];
        audioSource.Play();
    }
}

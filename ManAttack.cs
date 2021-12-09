using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAttack : MonoBehaviour
{
    AudioSource audioSource;

    public float timeBetweenAttacks = 2f;     // 攻击时间间隔
    public int attackDamage = 20;               // 每次攻击伤害
    public AudioClip[] attackAudio;              //存储攻击音效的数组
    public GameEnding gameEnding;              //引用游戏结束类

    GameObject player;                          // 引用玩家对象
    PlayerControl playerControl;                  // 引用玩家控制类


    bool playerInRange;                         // 用于表示玩家是否在攻击范围内的变量
    float timer = 0f;                         // 攻击间隔计时变量
    float timerSound = 0f;                           //音频播放计时变量



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        audioSource = GetComponent<AudioSource>();
    }




    void OnTriggerEnter(Collider other)
    {
        //若玩家进入SphereCollider的攻击范围
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        //若玩家离开SphereCollider的攻击范围
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 攻击计时变量不断增加
        timer += Time.deltaTime;

        //音频播放计时变量不断增加
        timerSound += Time.deltaTime;


        // 若攻击计时变量大于攻击间隔且玩家在攻击范围内且玩家没有胜利
        if (timer >= timeBetweenAttacks && playerInRange && !gameEnding.IsWin())
        {
            // 攻击
            Attack();
        }

        // 如果玩家生命值小于等于零，调用游戏结束类的设置失败方法，引发游戏失败
        if (playerControl.currentHealth <= 0)
        {
            gameEnding.CaughtPlayer();
        }
    }

    void Attack()
    {
        // 每次攻击时重置攻击间隔计时变量
        timer = 0f;


        //若音频计时变量很大了，给他重置一下，防止溢出造成意想不到的后果
        if (timerSound > 1000)
        {
            timerSound = 0f;
        }



        // 若玩家的生命值大于0
        if (playerControl.currentHealth > 0)
        {
            // 对玩家造成伤害
            playerControl.TakeDamage(attackDamage);

            // 通过对音频计时变量强制类型转换为int，与攻击音频数组长度取余的方法实现随机指定音频播放
            int n = (int)timerSound % attackAudio.Length;
            audioSource.clip = attackAudio[n];
            audioSource.Play();
        }
    }

    public bool isPlayerInRange()
    {
        return playerInRange;
    }



}

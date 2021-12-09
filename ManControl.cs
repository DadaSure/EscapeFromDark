using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManControl : MonoBehaviour
{
    Transform player;               // 引用玩家的位置
    PlayerControl playerControl;      // 引用玩家控制类
    NavMeshAgent nav;               // 引用NavMeshAgent
    float turnTimer=0f;
    public ManAttack manAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerControl = player.GetComponent<PlayerControl>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
         // 跟踪玩家
         nav.SetDestination(player.position);

        //朝向计时器自增
        turnTimer += Time.deltaTime;


        //朝向玩家，使用计时器，防止鬼畜
        if (turnTimer > 0.5f&&!manAttack.isPlayerInRange())
        {
            turnTimer = 0f;
            //使用Vector3.Angle方法求两个向量的夹角
            float angle = Vector3.Angle(transform.forward, player.position - transform.position);
            //使用Vector3.Cross方法求两个向量的叉乘后的值
            Vector3 v = Vector3.Cross(transform.forward, player.position - transform.position);
            //判断v.y的正负，使用左手螺旋法则来旋转Transform，使其朝向与它形成夹角的游戏对象
            if (v.y > 0)
            {
                transform.Rotate(Vector3.up * angle);
            }
            else
            {
                transform.Rotate(Vector3.down * angle);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressSpace : MonoBehaviour
{
    public CanvasGroup hint;            //引用闪动的文本CanvasGroup
    public float blinkDuration =1f;         //闪动周期
    float timer = 0f;           //计时器

    bool isIncreasing = true;
    bool isDecreasing = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //按下空格键进入主游戏场景
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            Debug.Log("LoadScene(1)");
        }


        //计时器应该增加时，就增加
        if (isIncreasing)
        {
            timer += Time.deltaTime;
        }

        //计时器应该减少时，就减少
        if (isDecreasing)
        {
            timer -= Time.deltaTime;
        }


        //计时器达到blinkDuration时就开始减少
        if (timer >= blinkDuration)
        {
            isDecreasing = true;
            isIncreasing = false;
        }

        //计时器回到0时就开始增加
        if (timer <= 0)
        {
            isIncreasing = true;
            isDecreasing = false;
        }

        //计时器值合法时，将闪动图片的alpha进行更改，实现闪动效果
        if (timer >= 0 && timer <= blinkDuration)
        {
            hint.alpha = timer / blinkDuration;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    //時間計算用変数
    private float Delta;
    //EffectDestroy時間
    private float DestroyTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Delta += Time.deltaTime;    //時間計算開始
        //生成後2.0秒
        if(Delta >= DestroyTime)
        {
            Destroy(gameObject);    //Effect破壊
        }
    }
}

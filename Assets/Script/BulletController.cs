using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //時間計算用変数
    private float Delta;
    //Bullet破壊時間
    private float DestroyTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Delta += Time.deltaTime;    //時間計算
        //生成2秒後に破壊
        if(Delta >= DestroyTime)
        {
            Destroy(gameObject);    //BulletEffect破壊
        }
    }
}

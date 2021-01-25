using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    //各GameObjectを取得
    public GameObject PlayerSword;
    public GameObject PlayerGun;
    //ButtonManagerのScriptの取得
    private ButtonManager ButtonManagerScr;

    //Playerの初期位置
    private float DefaultPosX = -7.0f;
    private float DefaultPosY = -3.3f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerSwordObj = Instantiate(PlayerSword);                                 //PlayerSwordを生成する
        PlayerSwordObj.transform.position = new Vector3(DefaultPosX, DefaultPosY, 0);         //PlayerSwordの生成位置指定
        ButtonManagerScr = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();    //ButtonManagerのScriptの取得
        ButtonManagerScr.PlayerSwordGenerate();                                               //PlayerSwordの生成完了をButtonManagerに共有
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

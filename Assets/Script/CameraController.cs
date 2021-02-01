using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //自身のTransformを取得
    private Transform myTransform;
    //自身の移動限度
    private float MaxX = 200.0f;
    private float MinX = 0.0f;
    //自身のY･Z定位置
    private float myPosY = 0.0f;
    private float myPosZ = -10.0f;
    //追従時のCameraの位置
    private Vector3 CameraPos;
    //追従時Playerとの補間の強さ
    private float Follow = 1.0f;

    //PlayerSword･GunのGameObject･Transform･Scriptを取得
    private GameObject PlayerSword;
    private GameObject PlayerGun;
    private Transform PlayerSwordTra;
    private Transform PlayerGunTra;
    private PlayerSwordController PlayerSwordScr;
    private PlayerGunController PlayerGunScr;
    //PlayerGeneratorのScriptを取得
    private PlayerGenerator PlayerGeneratorScr;
    //Playerの生成状況把握用変数
    private bool Sword;
    private bool Gun;
    //Playerの現在値取得用変数
    private Vector3 PlayerPos;

    //BackGroundのTransform取得
    private Transform BackGroundTra;
    //BackGroundのY･Z定位置
    private float BackGroundPosY = 0.0f;
    private float BackGroundPosZ = 20.0f;
    //追従時のBackGroundの位置
    private Vector3 BackGroundPos;
    //追従時BackGroundとカメラの移動速度の倍率
    private float BackGroundRate = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;                                                                           //自身のTransformの取得
        PlayerGeneratorScr = GameObject.FindWithTag("PlayerGenerator").GetComponent<PlayerGenerator>();    //PlayerGeneratorのScriptの取得
        PlayerSword = GameObject.FindWithTag("PlayerSword");                                               //PlayerSwordのGameObject取得
        PlayerSwordTra = PlayerSword.transform;                                                            //PlayerSwordのTransform取得
        PlayerSwordScr = PlayerSword.GetComponent<PlayerSwordController>();                                //PlayerSwordのScript取得
        Sword = PlayerGeneratorScr.Sword;                                                                  //PlayerGeneratorと生成状況の共有
        Gun = PlayerGeneratorScr.Gun;                                                                      //PlayerGeneratorと生成状況の共有
        BackGroundTra = GameObject.FindWithTag("BackGround").GetComponent<Transform>();                    //BackGroundのTransformを取得
    }

    // Update is called once per frame
    void Update()
    {

    }
    //カクつき防止
    void LateUpdate()
    {
        //Player生成状況の判別
        if (Sword == true)
        {
            PlayerPos = PlayerSwordTra.position;                                                                     //現在のposition取得
        }
        else if (Gun == true)
        {
            PlayerPos = PlayerGunTra.position;                                                                       //現在のposition取得
        }
        //自身のpositionが限度未満の時
        if (PlayerPos.x >= MinX && PlayerPos.x <= MaxX)
        {
            CameraPos = new Vector3(PlayerPos.x, myPosY, myPosZ);                                                    //Cameraの目的地を指定
            myTransform.position = Vector3.Lerp(myTransform.position, CameraPos, Follow);                            //Cameraの追従
            BackGroundPos = new Vector3(myTransform.position.x * BackGroundRate, BackGroundPosY, BackGroundPosZ);    //Cameraの目的地を指定
            BackGroundTra.position = Vector3.Lerp(BackGroundTra.position, BackGroundPos, Follow);                    //BackGroundの追従
        }
    }
    //PlayerSword生成
    public void PlayerSwordGenerate()
    {
        PlayerSword = GameObject.FindWithTag("PlayerSword");                   //PlayerSwordのGameObject取得
        PlayerSwordTra = PlayerSword.transform;                                //PlayerSwordのTransform取得
        PlayerSwordScr = PlayerSword.GetComponent<PlayerSwordController>();    //PlayerSwordのScript取得
        Sword = PlayerGeneratorScr.Sword;                                      //PlayerGeneratorと生成状況の共有
        Gun = PlayerGeneratorScr.Gun;                                          //PlayerGeneratorと生成状況の共有
    }
    //PlayerGun生成
    public void PlayerGunGenerate()
    {
        PlayerGun = GameObject.FindWithTag("PlayerGun");                        //PlayerGunのGameObject取得
        PlayerGunTra = PlayerGun.transform;                                     //PlayerGunのTransform取得
        PlayerGunScr = PlayerGun.GetComponent<PlayerGunController>();           //PlayerGunのScript取得
        Sword = PlayerGeneratorScr.Sword;                                       //PlayerGeneratorと生成状況の共有
        Gun = PlayerGeneratorScr.Gun;                                           //PlayerGeneratorと生成状況の共有
    }
}

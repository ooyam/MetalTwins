using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningTreeController : MonoBehaviour
{
    //自身のParticle取得
    private ParticleSystem myParticle;
    //現在の自身のPosition取得
    private Vector3 myPosition;
    //SwordAttackEffect取得
    public GameObject AttackEffect;
    //SwordEffect生成位置の修正値
    private float CorriectionPosX = 1.0f;
    private float CorriectionPosY = 2.0f;
    private float CorriectionPosZ = 2.5f;
    //燃焼命令用変数
    private bool Burning;
    //時間計算用変数/自身を破壊するまでの時間
    private float Delta;
    private float DestroyTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        myPosition = transform.position;                //自身のPosition取得
        myParticle = GetComponent<ParticleSystem>();    //自身のPartricleSystem取得
    }

    // Update is called once per frame
    void Update()
    {
        //燃焼開始時
        if(Burning == true)
        {
            Delta += Time.deltaTime;    //時間計算開始
            //燃焼開始から1.0秒後
            if(Delta >= DestroyTime)
            {
                Destroy(gameObject);    //自身を破壊
            }
        }
    }
    //Swordと接触した時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            Vector3 CutPoint = other.ClosestPointOnBounds(this.transform.position);    //切断箇所取得
            GameObject AttackEffectObj = Instantiate(AttackEffect);                                                              //AttackEffect生成
            AttackEffectObj.transform.position = CutPoint;                              //AttackEffectのPositionを指定
            myParticle.Play();                                                                                                   //Particle再生
            Burning = true;                                                                                                      //燃焼開始
            Debug.Log(CutPoint);
        }
    }
}

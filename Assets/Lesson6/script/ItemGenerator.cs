using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefanを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private float startPos = 30.0f;
    //ゴール地点
    private float goalPos = 300.0f;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //アイテムを生成する位置（プレイヤーからの距離）
    private float itemGenerateDistance = 50.0f;
    //unityちゃんの進んだ距離を計算する為の基準点
    private Vector3 basePos;


    // Start is called before the first frame update
    void Start()
    {
        //unityちゃんの初期地点の座標を基準点として取得
        basePos = GameObject.Find("unitychan").transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの現在の座標を取得
        Vector3 nowPos = GameObject.Find("unitychan").transform.position;
        //現在地点と初期地点との距離
        float distance = nowPos.z - basePos.z;


        //unityちゃんが15m進む毎にアイテムを生成する。

        //距離が15m以上になった時
        if (distance >= 15.0f　&&　nowPos.z > startPos && nowPos.z < goalPos)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-10, 1);
                //コーンをx軸方向に一直線に生成
                for (float j = -1f; j <= 1f; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, nowPos.z + itemGenerateDistance + offsetZ);
                    //現在地点を基準点として更新する
                    basePos = nowPos;
                    if (cone.transform.position.z < nowPos.z)
                    {
                        GameObject.Destroy(cone);
                    }
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-10, 1);
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, nowPos.z + itemGenerateDistance + offsetZ);
                        //現在地点を基準点として更新する
                        basePos = nowPos;
                        if(coin.transform.position.z < nowPos.z)
                        {
                            GameObject.Destroy(coin);
                        }
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, nowPos.z + itemGenerateDistance + offsetZ);
                        //現在地点を基準点として更新する
                        basePos = nowPos;
                    }
                }
            }
        }
    }
}
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
    private float startPos = 80.0f;
    //ゴール地点
    private float goalPos = 360.0f;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //アイテムを生成する位置（プレイヤーからの距離）
    private float itemGenerateDistance = 50.0f;
    //最後に生成したアイテムの座標
    public Vector3 lastItem;

    // Start is called before the first frame update
    void Start()
    {
        //アイテム生成の基準の座標
        lastItem = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの現在の座標を取得
        Vector3 nowPos = GameObject.Find("unitychan").transform.position;
        //現在地点と最後に生成したアイテムの距離
        float distance = lastItem.z - nowPos.z;

        //生成したアイテムとunityちゃんとの距離が35m以内且つ、ゴールしてない時
        if (distance <= 35.0f && nowPos.z <= goalPos - itemGenerateDistance)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1f; j <= 1f; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, nowPos.z + itemGenerateDistance);
                    lastItem = cone.transform.position;
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-10, 1);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, nowPos.z + itemGenerateDistance + offsetZ);
                        lastItem = coin.transform.position;
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, nowPos.z + itemGenerateDistance + offsetZ);
                        lastItem = car.transform.position;
                    }
                }
            }
        }
    }
}
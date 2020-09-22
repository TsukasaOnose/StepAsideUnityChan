using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowPos = GameObject.Find("unitychan").transform.position;
        if ( this.transform.position.z < nowPos.z - 10)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyCoin), 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyCoin()
    {
        Destroy(gameObject);
    }
}

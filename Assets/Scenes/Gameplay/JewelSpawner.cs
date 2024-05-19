using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSpawner : Singleton<JewelSpawner>
{
    public GameObject[] jewelPrefabs;
    public Transform[] jewelPos;
    public GameObject jewelParent;
    public int amountOfJewels = 12;
    // Start is called before the first frame update
    void Start()
    {
        SpawnJewels();
    }

    public void SpawnJewels()
    {
        amountOfJewels = 12;
        for (int i = 0; i < amountOfJewels; i++)
        {
            GameObject jewelImage = Instantiate(jewelPrefabs[Random.Range(0, jewelPrefabs.Length)], jewelPos[i].position, Quaternion.identity);
            jewelImage.transform.SetParent(jewelParent.transform);
        }
    }
}

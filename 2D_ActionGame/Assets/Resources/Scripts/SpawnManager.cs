using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject goblin;
    public GameObject coin;
    public float startDelay;
    public float repeatTime;
    public float xRange = 10;
    public float minYRange = -1.8f;
    public float maxYRange = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, repeatTime);
        InvokeRepeating("SpawnCoin", startDelay, repeatTime * 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemy()
    {
        Instantiate(goblin, new Vector2(xRange, Random.Range(minYRange, maxYRange)),goblin.transform.rotation);
    }
    void SpawnCoin()
    {
        Instantiate(coin, new Vector2(10, Random.Range(minYRange, maxYRange)), coin.transform.rotation);
    }
}

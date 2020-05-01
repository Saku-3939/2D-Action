using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject goblin;
    public GameObject coin;
    public float startDelay;
    public float repeatTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemy()
    {
        Instantiate(goblin, new Vector2(10, Random.Range(-1.8f, 2.5f)),goblin.transform.rotation);
    }
}

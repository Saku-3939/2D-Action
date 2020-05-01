using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Image[] life;
    public int lifeCount;
    // Start is called before the first frame update
    void Start()
    {
        lifeCount = life.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LifeDamage()
    {
        life[lifeCount - 1].enabled = false;
        lifeCount--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    private float timer = 0;
    private float countTimer = 0;
    private bool isGenerated = false;

    private int enemyCount = 1; // this is the # of enemies we are going to generate
    public GameObject[] enemies; // this is the possible enemy list    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGenerated)
        {
            generateEnemies();
            isGenerated = true;
        }
        else
        {
            timer += Time.deltaTime;
            countTimer += Time.deltaTime;
            if (timer > 10)
            {
                generateEnemies();
                timer = 0;
            }
            if (countTimer > 30)
            {
                enemyCount++;
                countTimer = 0;
            }
        }
    }

    private void generateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float rx = Random.Range(-50, 50);
            float rz = Random.Range(100, 150);
            int idx = Random.Range(0, enemies.Length);
            GameObject obj = Instantiate(enemies[idx]);
            obj.transform.position = new Vector3(rx, 0, rz);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPooints;

    public GameObject enemyBug;
    public GameObject enemySpider;

    private int randomEnemy;

    public float minTimeToSpawn = 2f;
    public float maxTimeToSpawn = 4f;

    private int number;

    [Header("Bonus")]
    public GameObject virusBonus;
    public GameObject ballBonus;


    private void Start()
    {

        StartCoroutine(SpawnEnemy());

        StartCoroutine(SpawnBonuus());

    }


    private IEnumerator SpawnEnemy()
    {

        number = Random.Range(0, spawnPooints.Length);

        randomEnemy = Random.Range(0 , 2);

        //int nubmer2 = Random.Range(0, spawnPooints.Length);

        if (randomEnemy == 0)
        {
            Instantiate(enemyBug, spawnPooints[number].position, Quaternion.identity);
        }
        else if (randomEnemy == 1)
        {
            Instantiate(enemySpider, spawnPooints[number].position, Quaternion.identity);
        }

        yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));

        StartCoroutine(SpawnEnemy());

    }


    private IEnumerator SpawnBonuus()
    {

        yield return new WaitForSeconds(Random.Range(minTimeToSpawn + 4, maxTimeToSpawn + 4));

        spawnBonus();

        StartCoroutine(SpawnBonuus());

    }


    private void spawnBonus()
    {

        int numberForBonus = Random.Range(0, spawnPooints.Length);

        if(numberForBonus == number)
        {
            spawnBonus();
            return;
        }

        randomEnemy = Random.Range(0, 2);

        if (randomEnemy == 0)
        {
            Instantiate(virusBonus, spawnPooints[numberForBonus].position, Quaternion.identity);
        }
        else if (randomEnemy == 1)
        {
            Instantiate(ballBonus, spawnPooints[numberForBonus].position, Quaternion.identity);
        }

    }



}

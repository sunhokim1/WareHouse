using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public Transform boxPrefab;
    public Transform spawnPoint;
    public float countdown = 2f;
    public float timeBetweenWaves = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        
    }

    IEnumerator SpawnWave()
    {
  
        SpawnBox();
        yield return new WaitForSeconds(0.5f);

    }

    void SpawnBox()
    {
        Instantiate(boxPrefab,spawnPoint.position,spawnPoint.rotation);
    }
}

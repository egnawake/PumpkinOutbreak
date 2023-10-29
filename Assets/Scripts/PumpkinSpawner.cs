using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Pumpkin pumpkinPrefab;
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float maxInterval = 4f;

    public void Start()
    {
        StartCoroutine(SpawnPeriodic());
    }

    private IEnumerator SpawnPeriodic()
    {
        while (true)
        {
            int amount = Random.Range(0, 4);
            Vector3 spawnPoint = RandomSpawnPoint();
            for (int i = 0; i < amount; i++)
            {
                Pumpkin p = Instantiate(pumpkinPrefab, spawnPoint, Quaternion.identity);
                p.Setup(player.transform);
            }
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }

    private Vector3 RandomSpawnPoint()
    {
        Vector2 point = Random.insideUnitCircle.normalized;
        point = point * Random.Range(15f, 20f);
        return new Vector3(point.x, 0, point.y);
    }
}

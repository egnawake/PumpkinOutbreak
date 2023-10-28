using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Pumpkin pumpkinPrefab;

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            Pumpkin p = Instantiate(pumpkinPrefab, pos, Quaternion.identity);
            p.Setup(player.transform);
        }
    }
}

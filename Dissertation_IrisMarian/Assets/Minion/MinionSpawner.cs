using System.Collections;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public float minionSpeed;

    public GameObject minionPrefab;
    //public Transform spawnPoint;
    public float waveSpawnInterval;
    public float minionSpawnInterval;
    public int minionsperWave;

    private int waveCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnMinions());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnMinions()
    {
        while(true)
        {
            ++waveCount;

            for (int i = 0; i < minionsperWave; i++)
            {
                spawnMinion();
                yield return new WaitForSeconds(minionSpawnInterval);
            }

            yield return new WaitForSeconds(waveSpawnInterval);
        }
    }

    private void spawnMinion()
    {
        GameObject minion = Instantiate(minionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        minion.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = minionSpeed;
    }
}

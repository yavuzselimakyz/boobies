using System.Collections;
using UnityEngine;

public class PUFPUFspawn : MonoBehaviour
{
    public GameObject objectToSpawn; // Spawn edilecek nesne
    public Transform spawnPoint; // Nesnenin spawn edilece�i yer
    public float spawnCooldown = 2f; // Spawn s�resi (cooldown)

    private bool canSpawn = true;

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnObject());
        }
    }

    IEnumerator SpawnObject()
    {
        canSpawn = false;
        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
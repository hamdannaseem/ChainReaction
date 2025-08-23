using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SmallSize, BigSize, DropHeight, ArenaLimits;
    public Material[] Materials;
    public GameObject[] Prefabs;
    float SpawnSize, RandomSize;
    Stack<GameObject> pool = new Stack<GameObject>();
    public void SetSize(bool Big)
    {
        if (Big)
        {
            SpawnSize = BigSize;
        }
        else
        {
            SpawnSize = SmallSize;
        }
    }
    public void SetRandomSize()
    {
        if (Random.Range(BigSize, SmallSize) > ((BigSize + SmallSize) / 2))
        {
            SpawnSize = SmallSize;
        }
        else
        {
            SpawnSize = BigSize;
        }
    }
    public void Spawn(int PrefabIndex)
    {
        if (PrefabIndex > Prefabs.Length) { return; }
        Vector3 SpawnPos = new Vector3(Random.Range(ArenaLimits, -ArenaLimits), DropHeight, Random.Range(ArenaLimits, -ArenaLimits));
        GameObject spawnedPrefab = Instantiate(Prefabs[PrefabIndex], SpawnPos, Quaternion.identity);
        spawnedPrefab.GetComponent<Renderer>().material = Materials[Random.Range(0, Materials.Length)];
        spawnedPrefab.transform.localScale = spawnedPrefab.transform.localScale * SpawnSize;
        pool.Push(spawnedPrefab);
    }
    public void Spawn()
    {
        Spawn(Random.Range(0, Prefabs.Length));
    }
    public void Remove()
    {
        if (pool.Count == 0) { return; }
        Destroy(pool.Peek());
        pool.Pop();
    }
}

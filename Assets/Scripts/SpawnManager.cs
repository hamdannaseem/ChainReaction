using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager AccessInstance;
    [Header("Spawn Settings")]
    [SerializeField] private float SmallSize;
    [SerializeField] private float BigSize;
    [SerializeField] private float DropHeight;
    [SerializeField] private float ArenaLimits;

    [Header("References")]
    [SerializeField] private Material[] Materials;
    public GameObject[] Prefabs;
    private float SpawnSize;

    public void SetSize(bool Big) => SpawnSize = Big ? BigSize : SmallSize;
    public void SetRandomSize() => SpawnSize = Random.value > .5f ? SmallSize : BigSize;

    void Awake()
    {
        AccessInstance = this;
    }
    public void Spawn(int PrefabIdx)
    {
        if (PrefabIdx >= Prefabs.Length || PrefabIdx < 0) { return; }
        Vector3 SpawnPos = new Vector3(Random.Range(-ArenaLimits, ArenaLimits), DropHeight, Random.Range(-ArenaLimits, ArenaLimits));
        GameObject NewShape;
        PooledObject pooledObject;
        if (PoolManager.AccessInstance.InactiveCount(PrefabIdx) == 0)
        {
            NewShape = Instantiate(Prefabs[PrefabIdx]);
            pooledObject = NewShape.AddComponent<PooledObject>();
            pooledObject.PrefabIdx = PrefabIdx;
            pooledObject.Renderer = NewShape.GetComponent<Renderer>();
            pooledObject.Name = NewShape.GetComponentInChildren<TextMeshPro>();
        }
        else
        {
            NewShape = PoolManager.AccessInstance.Dequeue(PrefabIdx);
            pooledObject = NewShape.GetComponent<PooledObject>();
        }
        NewShape.transform.position = SpawnPos;
        pooledObject.Renderer.sharedMaterial = Materials[Random.Range(0, Materials.Length)];
        NewShape.transform.localScale = DefaultSize(PrefabIdx) * SpawnSize;
        pooledObject.Name.text = GetName(pooledObject, PrefabIdx);
        NewShape.SetActive(true);
        PoolManager.AccessInstance.Push(NewShape);
    }
    public void Spawn()
    {
        Spawn(Random.Range(0, Prefabs.Length));
    }
    Vector3 DefaultSize(int PrefabIdx)
    {
        return Prefabs[PrefabIdx].transform.localScale;
    }
    public string GetName(PooledObject pooledObject, int PrefabIdx)
    {
        return pooledObject.Renderer.sharedMaterial.name +" "+ Prefabs[PrefabIdx].name;
    }
    public void Remove()
    {
        PoolManager.AccessInstance.Pop();
    }
}
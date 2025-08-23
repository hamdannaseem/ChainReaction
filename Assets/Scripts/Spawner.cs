using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float SmallSize;
    [SerializeField] private float BigSize;
    [SerializeField] private float DropHeight;
    [SerializeField] private float ArenaLimits;
    [SerializeField] private int PoolSize;

    [Header("References")]
    [SerializeField] private Material[] Materials;
    [SerializeField] private GameObject[] Prefabs;
    [SerializeField] private Button[] SpawnButtons;

    private Dictionary<int, Queue<GameObject>> InactivePools;
    private Stack<GameObject> ActivePool;
    private float SpawnSize;

    public void SetSize(bool Big) => SpawnSize = Big ? BigSize : SmallSize;
    public void SetRandomSize() => SpawnSize = Random.value > .5f ? SmallSize : BigSize;

    void Start()
    {
        InactivePools = new Dictionary<int, Queue<GameObject>>();
        ActivePool = new Stack<GameObject>();
        for (int i = 0; i < Prefabs.Length; i++)
        {
            InactivePools[i] = new Queue<GameObject>();
        }
    }
    public void Spawn(int PrefabIdx)
    {
        if (PrefabIdx >= Prefabs.Length || PrefabIdx < 0) { return; }
        Vector3 SpawnPos = new Vector3(Random.Range(-ArenaLimits, ArenaLimits), DropHeight, Random.Range(-ArenaLimits, ArenaLimits));
        GameObject NewShape;
        PooledObject pooledObject;
        if (InactivePools[PrefabIdx].Count == 0)
        {
            NewShape = Instantiate(Prefabs[PrefabIdx]);
            pooledObject = NewShape.AddComponent<PooledObject>();
            pooledObject.PrefabIdx = PrefabIdx;
            pooledObject.Renderer = NewShape.GetComponent<Renderer>();
        }
        else
        {
            NewShape = InactivePools[PrefabIdx].Dequeue();
            pooledObject = NewShape.GetComponent<PooledObject>();
        }
        NewShape.transform.position = SpawnPos;
        pooledObject.Renderer.sharedMaterial = Materials[Random.Range(0, Materials.Length)];
        NewShape.transform.localScale = Vector3.one * SpawnSize;
        NewShape.SetActive(true);
        ActivePool.Push(NewShape);
        if (ActivePool.Count == PoolSize)
        {
            SetInteractable(false);
        }
    }
    public void Spawn()
    {
        Spawn(Random.Range(0, Prefabs.Length));
    }
    public void Remove()
    {
        if (ActivePool.Count == 0) { return; }
        if (ActivePool.Count == PoolSize)
        {
            SetInteractable(true);
        }
        GameObject LastShape = ActivePool.Pop();
        LastShape.SetActive(false);
        InactivePools[LastShape.GetComponent<PooledObject>().PrefabIdx].Enqueue(LastShape);
    }
    void SetInteractable(bool state)
    {
        foreach (Button button in SpawnButtons)
        {
            button.interactable = state;
        }
    }
}
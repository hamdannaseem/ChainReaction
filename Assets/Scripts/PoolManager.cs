using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager AccessInstance;
    private Dictionary<int, Queue<GameObject>> InactivePools;
    private List<GameObject> ActivePool;
    public int PoolSize;
    void Start()
    {
        AccessInstance = this;
        InactivePools = new Dictionary<int, Queue<GameObject>>();
        ActivePool = new List<GameObject>();
        for (int i = 0; i < SpawnManager.AccessInstance.Prefabs.Length; i++)
        {
            InactivePools[i] = new Queue<GameObject>();
        }
    }
    public void Push(GameObject NewShape)
    {
        ActivePool.Add(NewShape);
        if (ActivePool.Count == PoolSize)
        {
            UIManager.AccessInstance.SetInteractable(false);
        }
    }
    public void Pop()
    {
        if (ActivePool.Count == 0) { return; }
        if (ActivePool.Count == PoolSize)
        {
            UIManager.AccessInstance.SetInteractable(true);
        }
        GameObject LastShape = ActivePool[ActivePool.Count - 1];
        ActivePool.RemoveAt(ActivePool.Count - 1);
        LastShape.SetActive(false);
        InactivePools[LastShape.GetComponent<PooledObject>().PrefabIdx].Enqueue(LastShape);
    }
    public GameObject Dequeue(int PrefabIdx)
    {
        return InactivePools[PrefabIdx].Dequeue();
    }
    public int InactiveCount(int PrefabIdx)
    {
        return InactivePools[PrefabIdx].Count;
    }
    public int ActiveCount()
    {
        return ActivePool.Count;
    }
    public List<GameObject> GetActive()
    {
        return ActivePool;
    }
}

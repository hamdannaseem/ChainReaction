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
        UIManager.AccessInstance.SetCount(ActiveCount(), PoolSize);
    }
    public void Push(GameObject NewShape)
    {
        ActivePool.Add(NewShape);
        if (ActivePool.Count == PoolSize)
        {
            UIManager.AccessInstance.SetInteractable(false);
        }
        UIManager.AccessInstance.SetCount(ActiveCount(), PoolSize);
    }
    public void Pop()
    {
        if (ActivePool.Count == 0) { return; }
        if (ActivePool.Count == PoolSize)
        {
            UIManager.AccessInstance.SetInteractable(true);
        }
        GameObject LastShape = ActivePool[ActivePool.Count - 1];
        ReturnToPool(ActivePool.Count - 1);
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
    public void Kill(GameObject Killed)
    {
        int Index = ActivePool.IndexOf(Killed);
        if (Index != -1) ReturnToPool(Index);
        if (ActiveCount() == 1)
        {
            ReactionManager.AccessInstance.StopReaction(true);
        }
    }
    void ReturnToPool(int PrefabIdx)
    {
        GameObject ToReturn = ActivePool[PrefabIdx];
        ActivePool.Remove(ToReturn);
        ToReturn.SetActive(false);
        Rigidbody rb = ToReturn.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ToReturn.GetComponent<ReactionObject>().ResetHealth();
        InactivePools[ToReturn.GetComponent<PooledObject>().PrefabIdx].Enqueue(ToReturn);
        UIManager.AccessInstance.SetCount(ActiveCount(), PoolSize);
    }
}

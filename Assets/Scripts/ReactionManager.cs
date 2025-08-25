using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public static ReactionManager AccessInstance;
    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private PhysicsMaterial Slippery, Grippy;
    private List<GameObject> ReactionComponents;
    public bool Reacting = false;
    public int ReactionTime;
    Coroutine Reaction;
    Vector3 DefaultGravity;
    void Start()
    {
        AccessInstance = this;
        DefaultGravity=Physics.gravity;
    }
    public void StartReaction()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y * 2, 0);
        ReactionComponents = Environment.Concat(PoolManager.AccessInstance.GetActive()).ToList();
        SetPhysicsMaterial(Slippery);
        Reacting = true;
        UIManager.AccessInstance.SetInteractable(false);
        Reaction = StartCoroutine(ReactionTimer());
    }
    IEnumerator ReactionTimer()
    {
        int remaining = ReactionTime;
        UIManager UM = UIManager.AccessInstance;
        while (remaining > 0 && Reacting)
        {
            yield return new WaitForSeconds(1);
            remaining--;
            UM.SetTime(remaining);
        }
        UM.SetScore(PoolManager.AccessInstance.ActiveCount() * 5);
        UM.SetScore(remaining * 2);
        StopReaction(true);
    }
    public void StopReaction(bool won)
    {
        if (Reacting) SetPhysicsMaterial(Grippy);
        Reacting = false;
        Physics.gravity = DefaultGravity;
        if(won)UIManager.AccessInstance.End();
    }
    void SetPhysicsMaterial(PhysicsMaterial physicsMaterial)
    {
        foreach (GameObject ReactionComponent in ReactionComponents)
        {
            ReactionComponent.GetComponent<Collider>().sharedMaterial = physicsMaterial;
        }
    }
}

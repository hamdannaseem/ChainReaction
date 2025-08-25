using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public static ReactionManager AccessInstance;
    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private PhysicsMaterial Slippery, Grippy;
    [SerializeField] private Settings settings;
    private List<GameObject> ReactionComponents;
    public bool Reacting = false;
    public int ReactionTime, RemainingTime;
    Coroutine Reaction;
    Vector3 DefaultGravity;
    void Start()
    {
        AccessInstance = this;
        DefaultGravity = Physics.gravity;
        ReactionTime = settings.ReactionTime;
    }
    public void StartReaction()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y * 2, 0);
        ReactionComponents = Environment.Concat(PoolManager.AccessInstance.GetActive()).ToList();
        SetPhysicsMaterial(Slippery);
        Reacting = true;
        UIManager UM = UIManager.AccessInstance;
        UM.SetInteractable(false);
        UM.EnableTouch(false);
        Reaction = StartCoroutine(ReactionTimer());
    }
    IEnumerator ReactionTimer()
    {
        RemainingTime = ReactionTime;
        UIManager UM = UIManager.AccessInstance;
        while (RemainingTime > 0 && Reacting)
        {
            UM.SetTime(RemainingTime);
            yield return new WaitForSeconds(1);
            RemainingTime--;
        }
        if (Reacting)
        {
            StopReaction(true);
        }
    }
    public void StopReaction(bool won)
    {
        if (Reacting) SetPhysicsMaterial(Grippy);
        Reacting = false;
        Physics.gravity = DefaultGravity;
        if (won)
        {
            UIManager UM = UIManager.AccessInstance;
            UM.SetScore(PoolManager.AccessInstance.ActiveCount() * 5);
            UM.SetScore(RemainingTime * 2);
            UM.SetTime(RemainingTime);
            UM.End();
        }
    }
    void SetPhysicsMaterial(PhysicsMaterial physicsMaterial)
    {
        foreach (GameObject ReactionComponent in ReactionComponents)
        {
            ReactionComponent.GetComponent<Collider>().sharedMaterial = physicsMaterial;
        }
    }
}

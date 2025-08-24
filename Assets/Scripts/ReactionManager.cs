using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public static ReactionManager AccessInstance;
    [SerializeField] private List<GameObject> Environment;
    [SerializeField] private PhysicsMaterial Slippery, Grippy;
    private List<GameObject> ReactionComponents;
    public bool ReactionStarted = false;
    void Start()
    {
        AccessInstance = this;
    }
    public void StartReaction()
    {
        ReactionComponents = Environment.Concat(PoolManager.AccessInstance.GetActive()).ToList();
        ConstraintYAxis();
        SetPhysicsMaterial(Slippery);
        ReactionStarted = true;
        UIManager.AccessInstance.SetInteractable(false);
    }
    void SetPhysicsMaterial(PhysicsMaterial physicsMaterial)
    {
        foreach (GameObject ReactionComponent in ReactionComponents)
        {
            ReactionComponent.GetComponent<Collider>().sharedMaterial = physicsMaterial;
        }
    }
    void ConstraintYAxis()
    {
        foreach (GameObject Shape in PoolManager.AccessInstance.GetActive())
        {
            Rigidbody rb = Shape.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}

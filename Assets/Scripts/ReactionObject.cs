using UnityEngine;

public class ReactionObject : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    void Start() { rb = GetComponent<Rigidbody>(); }
    void OnMouseDown()
    {
        if (!ReactionManager.AccessInstance.ReactionStarted && PoolManager.AccessInstance.ActiveCount() == PoolManager.AccessInstance.PoolSize)
        {
            ReactionManager.AccessInstance.StartReaction();
            React(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (ReactionManager.AccessInstance.ReactionStarted)
        {
            React(collision.relativeVelocity.normalized);
        }
    }
    public void React(Vector3 direction)
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
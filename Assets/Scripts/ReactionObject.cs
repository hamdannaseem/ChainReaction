using UnityEngine;

public class ReactionObject : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private int Health;
    int DefaultHealth;
    Rigidbody rb;
    void Start() { rb = GetComponent<Rigidbody>(); DefaultHealth = Health; }
    void OnMouseDown()
    {
        if (!ReactionManager.AccessInstance.Reacting && PoolManager.AccessInstance.ActiveCount() == PoolManager.AccessInstance.PoolSize)
        {
            ReactionManager.AccessInstance.StartReaction();
            React(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!ReactionManager.AccessInstance.Reacting) { return; }
        React(collision.relativeVelocity.normalized);
        if (collision.transform.tag == "Shape" && Health != 0)
        {
            Health--;
        }
        else if (collision.transform.tag == "Shape" && Health == 0)
        {
            EmojiEmitter.AccessInstance.EmitAt(gameObject.transform.position);
            PoolManager.AccessInstance.Kill(this.gameObject);
            UIManager.AccessInstance.SetScore(10);
        }
    }
    public void React(Vector3 direction)
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
    public void ResetHealth()
    {
        Health = DefaultHealth;
    }
}
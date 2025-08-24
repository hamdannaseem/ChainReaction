using UnityEngine;

public class NameStabilizer : MonoBehaviour
{
    Transform Parent;
    void Start()
    {
        Parent = transform.parent;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(Parent.position.x + 0, Parent.position.y + 1, Parent.position.z + 0);
        transform.rotation = Quaternion.identity;
    }
}

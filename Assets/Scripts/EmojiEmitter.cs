using UnityEngine;

public class EmojiEmitter : MonoBehaviour
{
    public static EmojiEmitter AccessInstance;
    ParticleSystem ps;
    void Start()
    {
        AccessInstance = this;
        ps = GetComponent<ParticleSystem>();
    }
    public void EmitAt(Vector3 position)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = new Vector3(position.x, 0.5f, position.z);
        emitParams.applyShapeToPosition = false;
        ps.Emit(emitParams, 1);
    }
}
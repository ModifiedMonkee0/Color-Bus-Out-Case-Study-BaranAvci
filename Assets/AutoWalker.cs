using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    public float moveSpeed = 2.2f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

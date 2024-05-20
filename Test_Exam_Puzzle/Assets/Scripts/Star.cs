using UnityEngine;

public class Star : MonoBehaviour
{
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    public void SetPos()
    {
        transform.position = startPos;
    }
}

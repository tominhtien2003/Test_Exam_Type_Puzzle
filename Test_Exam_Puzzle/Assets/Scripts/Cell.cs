using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector3 pos;
    public void SetPos(Vector3 newPos)
    {
        pos = newPos;
    }
    public Vector3 GetPos()
    {
        return pos;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class CreateRandomJewl : MonoBehaviour
{
    [SerializeField] private Transform topLimit, bottomLimit, leftLimit, rightLimit;
    [SerializeField] private Transform[] jewlS;
    private HashSet<string> jewlPos = new HashSet<string>();
    private void Awake()
    {
        CreateRandomJewl_();
    }
    public void CreateRandomJewl_()
    {
        foreach (Transform jewl in jewlS)
        {
            for (int i = 0; i < 2; i++)
            {
                string pos = "_";
                int x = 0, y = 0;
                do
                {
                    x = Random.Range((int)leftLimit.position.x, (int)rightLimit.position.x);
                    y = Random.Range((int)bottomLimit.position.y, (int)topLimit.position.y);
                    pos = x + "" + y;
                }
                while (jewlPos.Contains(pos));
                if (pos != "") jewlPos.Add(pos);
                Transform obj = Instantiate(jewl);
                obj.GetComponent<Jewl>().SetPos(new Vector3(x,y,0));
                obj.position = new Vector3(x, y, 0);
            }
        }
    }
}

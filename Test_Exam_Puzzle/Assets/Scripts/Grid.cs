using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Transform cellEvenPrefab;
    [SerializeField] private Transform cellOldPrefab;
    public static int[,] checkPos = new int[6, 6];
    private void OnEnable()
    {
        InitGrid();
        
    }
    private void Awake()
    {
        
    }
    private void InitGrid()
    {
        int check = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j= 0; j < 5; j++)
            {
                checkPos[i, j] = 0;
                Transform _cellTransform;
                if (check == 0)
                {
                    _cellTransform = Instantiate(cellOldPrefab);
                }
                else
                {
                    _cellTransform = Instantiate(cellEvenPrefab);
                }
                _cellTransform.position = new Vector3(i, j, 0);
                _cellTransform.GetComponent<Cell>().SetPos(new Vector3(i, j, 0));
                check = 1 - check;
            }
        }
    }
}

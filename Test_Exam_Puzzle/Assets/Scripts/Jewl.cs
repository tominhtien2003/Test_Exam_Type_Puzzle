using System.Collections;
using UnityEngine;

public class Jewl : MonoBehaviour
{
    [SerializeField] private LayerMask masks;
    private Vector3 pos;
    private bool isDragging;
    private SpriteRenderer sprite;
    private Vector2[] direcs = new Vector2[4];
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        direcs[0] = Vector2.down;
        direcs[1] = Vector2.up;
        direcs[2] = Vector2.left;
        direcs[3] = Vector2.right;
    }
    public void SetPos(Vector3 newPos)
    {
        pos = newPos;
    }
    public Vector3 GetPos()
    {
        return pos;
    }
    private void Update()
    {
        HandleDragJewl();
    }
    private bool CheckLimit(Transform _trasform)
    {
        return (transform.position.x < 5 && transform.position.x >= 0 && transform.position.y >= 0 && transform.position.y < 5);
    }
    private void HandleDragJewl()
    {
        if (isDragging)
        {
            
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            if (CheckLimit(transform))
            {
                int x = (int)Mathf.Round(transform.position.x);
                int y = (int)Mathf.Round(transform.position.y);
                
                if (Grid.checkPos[x,y] == 0)
                {
                    Grid.checkPos[x, y] = 1;
                    SetPos(new Vector3(x, y, 0));
                    DetectJewl();
                }
                else
                {
                    transform.position = pos;
                }

            }
            else
            {
                transform.position = pos;
            }
        }
    }
    private void DetectJewl()
    {
        if (!CheckLimit(transform)) return;
        foreach (Vector2 dir in direcs)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir, 2f, masks);
            foreach (var hit_ in hit)
            {
                if (hit_.collider.gameObject != gameObject)
                {
                    if (hit_.collider.gameObject.layer == gameObject.layer)
                    {
                        StartCoroutine(TimeDestroyJewl(hit_.collider.gameObject));
                    }
                }
            }
        }
    }
    private IEnumerator TimeDestroyJewl(GameObject _gameobject)
    {
        GameObject obj = GameObject.Find("Star");
        obj.transform.position = Vector3.Lerp(transform.position, _gameobject.transform.position, .5f);
        yield return new WaitForSeconds(.25f);
        obj.GetComponent<Animator>().SetTrigger("Rotate");
        yield return new WaitForSeconds(.25f);
        obj.GetComponent<Animator>().SetTrigger("Blink");
        yield return new WaitForSeconds(.1f);
        obj.GetComponent<Star>().SetPos();
        Destroy(_gameobject);
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        isDragging = true;
        sprite.sortingOrder = 1;
        if (CheckLimit(transform))
        {
            Grid.checkPos[(int)transform.position.x,(int)transform.position.y] = 0;
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
        sprite.sortingOrder = 0;
    }

}

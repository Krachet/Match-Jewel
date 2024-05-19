using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JewelDragAndDrop : MonoBehaviour
{
    public Vector3 initialPos;
    public Vector3 finalPos;
    [SerializeField] private float speed = 5.0f;

    private Vector3 offset;

    public static JewelDragAndDrop currentJewelDrag;
    public LayerMask jewel;
    public LayerMask grid;

    public string jewelType;

    GameObject gridHit;

    public GameObject coin;

    private bool isInBoard = false;


    private void Awake()
    {
        initialPos = this.transform.position;
        finalPos = initialPos;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMousePosition();
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, jewel);
        RaycastHit2D hitGrid = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, grid);
        Debug.Log(hit.collider.gameObject.name);
        if (hit.collider != null)
        {
            currentJewelDrag = this;
        }
        if (hitGrid.collider != null)
        {
            hitGrid.collider.GetComponent<Grid>().isEmpty = true;
            hitGrid.collider.GetComponent<Grid>().jewelContained = null;
            hitGrid.collider.GetComponent<Grid>().jewelContainedType = null;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + offset;

        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, grid);

        if (hit.collider != null)
        {
            gridHit = hit.collider.gameObject;
            finalPos = gridHit.transform.position;
        }
    }

    private void OnMouseUp()
    {
        int decreaseJewel = 1;
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, grid);

        if (hit.collider == null)
        {
            finalPos = initialPos;
        }
        else
        {
            if (gridHit.GetComponent<Grid>().isEmpty)
            {
                if (!isInBoard)
                {
                    JewelSpawner.Instance.amountOfJewels -= decreaseJewel;
                    if (JewelSpawner.Instance.amountOfJewels == 0)
                    {
                        JewelSpawner.Instance.SpawnJewels();
                    }
                }
                gridHit.GetComponent<Grid>().jewelContained = this;
                Debug.Log(gridHit.GetComponent<Grid>().jewelContained.gameObject.name);
                gridHit.GetComponent<Grid>().jewelContainedType = jewelType;
                gridHit.GetComponent<Grid>().isEmpty = false;
                int numberMatch = 0;
                for (int i = 1; i < 5; i++)
                {
                    Grid direction = GridController.Instance.TestDirection(gridHit.GetComponent<Grid>().x, gridHit.GetComponent<Grid>().y, i);
                    if (direction)
                    {
                        numberMatch++;
                        Debug.Log(direction.gameObject.name);
                        GameObject coin1 = Instantiate(coin, direction.transform.position, Quaternion.identity);
                        direction.jewelContainedType = null;
                        direction.isEmpty = true;
                        Destroy(direction.jewelContained.gameObject);
                    }
                    else
                    {
                        Debug.Log("No match");
                    }
                }
                if (numberMatch > 0)
                {
                    Score.Instance.score += numberMatch + 1;
                    GameObject coin2 = Instantiate(coin, finalPos, Quaternion.identity);
                    gridHit.GetComponent<Grid>().jewelContainedType = null;
                    gridHit.GetComponent<Grid>().isEmpty = true;
                    Destroy(this.gameObject);
                }
                isInBoard = true;
            }
            else
            {
                finalPos = initialPos;
            }
        }
        transform.position = finalPos;
        currentJewelDrag = null;
        if (gridHit)
        {
            gridHit = null;
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}

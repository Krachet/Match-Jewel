using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    public int x;
    public int y;
    public string jewelContainedType;
    public JewelDragAndDrop jewelContained;
    public bool isEmpty = true;
}

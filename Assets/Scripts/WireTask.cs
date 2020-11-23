using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireTask : MonoBehaviour
{
    //initialze lists for colors, right, and left wires.
    public List<Color> wireColors = new List<Color>();
    public List<WireScript> left = new List<WireScript>();
    public List<WireScript> right = new List<WireScript>();

    //member variables
    private List<Color> colorsAvailable;
    private List<int> wiresLeft; //STORE INDEXES
    private List<int> wiresRight;

    public WireScript CurrentDraggedWire = null;
    public WireScript CurrentHoveredWire = null;

    public bool taskCompleted = false;
    public MoveComponent fix;

    private void Start()
    {
        colorsAvailable = new List<Color>(wireColors);
        wiresLeft = new List<int>();
        wiresRight = new List<int>();
        fix = null;
        //initialize list of indexes
        for (int i = 0; i < left.Count; i++)
        {
            wiresLeft.Add(i);
            wiresRight.Add(i);
        }
        while (colorsAvailable.Count > 0 && wiresLeft.Count > 0 && wiresRight.Count > 0)
        {
            Color selected = colorsAvailable[Random.Range(0, colorsAvailable.Count)];
            int randLeft = Random.Range(0, wiresLeft.Count);
            int randRight = Random.Range(0, wiresRight.Count);
            left[wiresLeft[randLeft]].SetColor(selected);
            right[wiresRight[randRight]].SetColor(selected);

            colorsAvailable.Remove(selected);
            wiresLeft.RemoveAt(randLeft);
            wiresRight.RemoveAt(randRight);
        }
        StartCoroutine(CheckTaskCompletion());
    }
    private IEnumerator CheckTaskCompletion()
    {
        while (!taskCompleted)
        {
            int successfulWires = 0;

            for (int i = 0; i < right.Count; i++)
            {
                if (right[i].completed) { successfulWires++; }
            }
            if (successfulWires >= right.Count)
            {
                Debug.Log("TASK COMPLETED");
                transform.gameObject.SetActive(false);
                fix.myPlayer = true;
            }
            else
            {
                Debug.Log("TASK INCOMPLETED");
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}

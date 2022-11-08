using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBox : MonoBehaviour
{
    Vector3[] box;
    [SerializeField]
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        box = new Vector3[4];
    }

    // Update is called once per frame
    public void Draw(Color color)
    {
        lr.startColor = lr.endColor = color;
        lr.positionCount = 4;
        box[0] = Vector3.zero;
        box[1] = Vector3.up;
        box[2] = Vector3.up + Vector3.right;
        box[3] = Vector3.right;
        lr.SetPositions(box);
    }

    public void HideLines()
    {
        lr.positionCount = 0;
    }
}

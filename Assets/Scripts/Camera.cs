using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public int leftLimit;
    public int rightLimit;
    public int topLimit;
    public int BottomLimit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            transform.position = new Vector3(withinRangeX(target.transform.position.x), withinRangeY(target.transform.position.y), -10);
    }
    float withinRangeX(float pos)
    {
        if (pos >= rightLimit)
            return rightLimit - 0.1f;
        else if (pos <= leftLimit)
            return leftLimit + 0.1f;
        else return pos;
    }
    float withinRangeY(float pos)
    {
        if (pos >= topLimit)
            return topLimit - 0.1f;
        else if (pos <= BottomLimit)
            return BottomLimit + 0.1f;
        else return pos;
    }
}

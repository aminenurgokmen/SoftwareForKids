using UnityEngine;

public class CupScript : MonoBehaviour
{
    public bool isActive = false;
    bool moveToPlacement = false;
    bool done = false;


    void Update()
    {

        if (moveToPlacement)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.placementPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, GameManager.Instance.placementPoint.position) < 0.01f)
            {
                transform.position = GameManager.Instance.placementPoint.position;
                isActive = false;
                done = true;
                KettleOneScript kettle = FindFirstObjectByType<KettleOneScript>();
                kettle.isPourTea = true;
            }
        }
    }
    void OnMouseDown()
    {
        if (done || !isActive) return;
        Debug.Log("Cup Pressed");
        isActive = false;
        moveToPlacement = true;
    }
}

using UnityEngine;

public class TeaScript : MonoBehaviour
{
    public bool isActive = false;
    bool done = false;
    bool moveToPlacement = false;
    bool moveBackToStart = false;
    Vector3 startPosition;
    float animationTimer = 0f;
    float animationDuration = 4f; // Adjust this based on your animation length

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (moveToPlacement)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.placementPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, GameManager.Instance.placementPoint.position) < 0.01f)
            {
                transform.position = GameManager.Instance.placementPoint.position;
                moveToPlacement = false;
                // Play tea animation after reaching placement
                GetComponentInChildren<Animator>().SetTrigger("TeaReady");
                KettleOneScript kettle = FindFirstObjectByType<KettleOneScript>();
                if (kettle != null) kettle.GetComponentInChildren<Animator>().SetTrigger("Kettle");
                animationTimer = 0f;
            }
        }
        else if (done && !moveBackToStart)
        {
            // Wait for animation to complete
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationDuration)
            {
                moveBackToStart = true;
            }
        }
        else if (moveBackToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * 2f);
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                transform.position = startPosition; 
                ButtonScript button = FindFirstObjectByType<ButtonScript>();
                if (button != null) button.isActive2 = true;
                moveBackToStart = false;
               
                // Don't reset done - task is complete once
                // done = false;
                // isActive = false;
            }
        }
    }
    void OnMouseDown()
    {
        if (!isActive || done) return;
        Debug.Log("Tea Clicked");
        done = true;
        // Move the tea to placement point
        moveToPlacement = true;
    }
}


using UnityEngine;

public class KettleOneScript : MonoBehaviour
{
    bool isPlaced = false;
    public bool isPourTea;
    bool done = false;
    public Animator kettleAnimator;

    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }
    void Update()
    {
        if (isPlaced)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.placementPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, GameManager.Instance.placementPoint.position) < 0.01f)
            {
                transform.position = GameManager.Instance.placementPoint.position;
                isPlaced = false;
                done = true;
                FindFirstObjectByType<FaucetScript>().isFaucet = true;
            }
        }
        if (isPourTea)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.pourTeaPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, GameManager.Instance.pourTeaPoint.position) < 0.01f)
            {
                transform.position = GameManager.Instance.pourTeaPoint.position;
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("PourTea");

                isPourTea = false;
                done = true;
               // FindFirstObjectByType<CupScript>().isActive = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (done || isPlaced) return;

        Debug.Log("Item clicked!");
        isPlaced = true;
        GameManager.Instance.activeItem = this;
    }
}

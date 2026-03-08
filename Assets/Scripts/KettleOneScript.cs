using UnityEngine;

public class KettleOneScript : MonoBehaviour
{
    bool isPlaced = false;
    public bool isPourTea;
    bool done = false;
    public Animator kettleAnimator;
    private Animator animator;
    public GameObject stepone, stepTwo,stepOneText;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
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
                foreach (var station in FindObjectsByType<StationScript>(FindObjectsSortMode.None))
                {
                    if (station.type == StationType.Faucet)
                    {
                        station.isActive = true;
                        break;
                    }
                }
            }
        }

        if (isPourTea)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.pourTeaPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, GameManager.Instance.pourTeaPoint.position) < 0.01f)
            {
                transform.position = GameManager.Instance.pourTeaPoint.position;
                animator.enabled = true;
                animator.SetTrigger("PourTea");
                isPourTea = false;
                done = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (done || isPlaced || GameManager.Instance.inputBlocked) return;
stepone.SetActive(false);
stepOneText.SetActive(false);
stepTwo.SetActive(true);
        Debug.Log("Item clicked!");
        isPlaced = true;
        GameManager.Instance.activeItem = this;
    }
}

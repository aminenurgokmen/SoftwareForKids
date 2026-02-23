using UnityEngine;

public class KettleOneScript : MonoBehaviour
{
    bool isPlaced = false;
    bool done = false;

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
    }

    private void OnMouseDown()
    {
        if (done || isPlaced) return;

        Debug.Log("Item clicked!");
        isPlaced = true;
        GameManager.Instance.activeItem = this;
    }
}

using UnityEngine;

public class FaucetScript : MonoBehaviour
{
    public bool isFaucet = false;
        bool pour = false;
    bool done = false;

    void Update()
    {
        if (pour)
        {
            GameManager.Instance.activeItem.transform.position = Vector3.MoveTowards(GameManager.Instance.activeItem.transform.position, GameManager.Instance.faucetPoint.position, Time.deltaTime * 5f);
            if (Vector3.Distance(GameManager.Instance.activeItem.transform.position, GameManager.Instance.faucetPoint.position) < 0.01f)
            {
                GameManager.Instance.activeItem.transform.position = GameManager.Instance.faucetPoint.position;
                GetComponentInChildren<Animator>().SetTrigger("Pour");
                KettleOneScript kettle = FindFirstObjectByType<KettleOneScript>();
                kettle.GetComponentInChildren<Animator>().SetTrigger("Kettle");
                pour = false;
                done = true;
                var stove = FindFirstObjectByType<StoveScript>();
                if (stove != null) stove.isStove = true;
            }
           
        }
    }

    void OnMouseDown()
    {
        if (!isFaucet || done) return;

        var item = GameManager.Instance.activeItem;
        if (item == null) return;
        if (Vector3.Distance(item.transform.position, GameManager.Instance.placementPoint.position) >= 0.01f) return;

        isFaucet = false;
        pour = true;
    }
}

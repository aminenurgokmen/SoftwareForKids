using UnityEngine;

public class StoveScript : MonoBehaviour
{
    public bool isStove = false;
    bool moveToStove = false;
    bool done = false;

    void Update()
    {
        if (moveToStove)
        {
            GameManager.Instance.activeItem.transform.position = Vector3.MoveTowards(GameManager.Instance.activeItem.transform.position, GameManager.Instance.stovePoint.position, Time.deltaTime * 3f);
            if (Vector3.Distance(GameManager.Instance.activeItem.transform.position, GameManager.Instance.stovePoint.position) < 0.01f)
            {
                GetComponentInChildren<Animator>().SetTrigger("Waiting");
                GameManager.Instance.activeItem.transform.position = GameManager.Instance.stovePoint.position;
                moveToStove = false;
                done = true;
                var button = FindFirstObjectByType<ButtonScript>();
                if (button != null) button.isActive = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (!isStove || done) return;

        var item = GameManager.Instance.activeItem;
        if (item == null) return;
        if (Vector3.Distance(item.transform.position, GameManager.Instance.faucetPoint.position) >= 0.01f) return;

        isStove = false;
        moveToStove = true;
    }
}

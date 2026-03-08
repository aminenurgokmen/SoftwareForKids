using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public enum StationType { Faucet, Stove }

public class StationScript : MonoBehaviour
{
    public StationType type;
    public bool isActive = false;
    bool moving = false;
    bool done = false;
    public GameObject stepTwo, stepThree,stepTwoText,stepfour,stepThereeText;

    Transform TargetPoint => type == StationType.Faucet
        ? GameManager.Instance.faucetPoint
        : GameManager.Instance.stovePoint;

    Transform PrerequisitePoint => type == StationType.Faucet
        ? GameManager.Instance.placementPoint
        : GameManager.Instance.faucetPoint;

    float MoveSpeed => type == StationType.Faucet ? 5f : 3f;

    string AnimTrigger => type == StationType.Faucet ? "Pour" : "Waiting";

    void Update()
    {
        if (!moving) return;

        GameManager.Instance.activeItem.transform.position = Vector3.MoveTowards(
            GameManager.Instance.activeItem.transform.position,
            TargetPoint.position,
            Time.deltaTime * MoveSpeed);

        if (Vector3.Distance(GameManager.Instance.activeItem.transform.position, TargetPoint.position) < 0.01f)
        {
            GameManager.Instance.activeItem.transform.position = TargetPoint.position;
            GetComponentInChildren<Animator>().SetTrigger(AnimTrigger);
            moving = false;
            done = true;
            OnArrived();
        }
    }

    void OnArrived()
    {
        if (type == StationType.Faucet)
        {
            FindFirstObjectByType<KettleOneScript>().kettleAnimator.SetTrigger("Kettle");
            StartCoroutine(FaucetWaitRoutine());
        }
        else
        {
            if (stepThereeText != null) stepThereeText.SetActive(false);
            if (stepfour != null) stepfour.SetActive(true);

            var button = FindFirstObjectByType<ButtonScript>();
            if (button != null) button.isActive = true;
        }
    }

    IEnumerator FaucetWaitRoutine()
    {
        GameManager.Instance.inputBlocked = true;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.inputBlocked = false;

        if (stepTwo != null) stepTwo.SetActive(false);
        if (stepThree != null) stepThree.SetActive(true);
        if (stepTwoText != null) stepTwoText.SetActive(false);

        foreach (var station in FindObjectsByType<StationScript>(FindObjectsSortMode.None))
        {
            if (station.type == StationType.Stove)
            {
                station.isActive = true;
                break;
            }
        }
    }

    void OnMouseDown()
    {
        if (!isActive || done || GameManager.Instance.inputBlocked) return;

        var item = GameManager.Instance.activeItem;
        if (item == null) return;
        if (Vector3.Distance(item.transform.position, PrerequisitePoint.position) >= 0.01f) return;

        if (type == StationType.Stove)
        {
            if (stepThree != null) stepThree.SetActive(false);
        }

        isActive = false;
        moving = true;
    }
}

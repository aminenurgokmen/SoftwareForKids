using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isActive = false;
    public bool isActive2 = false;
    bool done = false;
    public Material startMat;
    public GameObject stepFour, stepFourText ,wait,stepFive, stepFiveText;

    void OnMouseDown()
    {
        if (isActive2)
        {
            Debug.Log("Button Reset");
            isActive2 = false;
            done = false;
            isActive = false;
            GetComponent<MeshRenderer>().material = startMat;
            transform.GetChild(0).gameObject.SetActive(false);
            var cup = FindFirstObjectByType<CupScript>();
            if (cup != null) cup.isActive = true;
            return;
        }

        if (!isActive || done || GameManager.Instance.inputBlocked) return;

        Debug.Log("Button Pressed");
        isActive = false;
        done = true;
        GetComponentInChildren<Animator>().SetTrigger("Fire");
        GetComponent<MeshRenderer>().material.color = Color.red;
        var tea = FindFirstObjectByType<TeaScript>();
        if (tea != null) tea.isActive = true;

        if (stepFourText != null) stepFourText.SetActive(false);
        if (stepFour != null) stepFour.SetActive(false);
        if (wait != null) wait.SetActive(true);
        StartCoroutine(ButtonWaitRoutine());
    }

    IEnumerator ButtonWaitRoutine()
    {
        GameManager.Instance.inputBlocked = true;
        yield return new WaitForSeconds(3f);
        GameManager.Instance.inputBlocked = false;

        if (wait != null) wait.SetActive(false);
        if (stepFive != null) stepFive.SetActive(true);
        if (stepFiveText != null) stepFiveText.SetActive(true);
    }
}

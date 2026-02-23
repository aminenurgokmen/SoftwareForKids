using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isActive = false;
    public bool isActive2 = false;
    bool done = false;
    public Material startMat;

    void Start()
    {
        //startMat = GetComponent<MeshRenderer>().material;
    }
    void OnMouseDown()
    {
        // 2. basış (kapatma)
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

        // 1. basış (çay koyma)
        if (!isActive || done) return;

        Debug.Log("Button Pressed");

        isActive = false;
        done = true;

        GetComponentInChildren<Animator>().SetTrigger("Fire");
        GetComponent<MeshRenderer>().material.color = Color.red;

        var tea = FindFirstObjectByType<TeaScript>();
        if (tea != null) tea.isActive = true;
    }
}

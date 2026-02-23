using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isActive = false;
    bool done = false;

    void OnMouseDown()
    {
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void ChoseClassTwoAlgorithmGame1()
    {
       SceneManager.LoadScene("AlgorithmGame1");
    }
    public void ChoseClassTwoAlgorithmGame2()
    {
        SceneManager.LoadScene("AlgorithmGame2");
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform placementPoint, faucetPoint, stovePoint, pourTeaPoint;
    public KettleOneScript activeItem;
    public bool inputBlocked = false;

    void Awake()
    {
        Instance = this;
    }
}

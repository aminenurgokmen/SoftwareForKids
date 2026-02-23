using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public Transform placementPoint, faucetPoint, stovePoint;
   public KettleOneScript activeItem;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
       
        
    }
}

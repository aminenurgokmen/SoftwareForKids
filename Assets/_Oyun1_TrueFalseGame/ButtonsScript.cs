using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
  [SerializeField] private Vector3 pressOffset = new Vector3(0f, -0.5f, -0.5f);
  [SerializeField] private bool isTrue;
  public GameObject Object;
  private Vector3 originalLocalPosition;
  private RabbitGameScript rabbitGame;

  void Start()
  {
      originalLocalPosition = Object.transform.localPosition;
      rabbitGame = FindAnyObjectByType<RabbitGameScript>();
  }
  void OnMouseDown()
  {
      Object.transform.localPosition = originalLocalPosition + pressOffset;
  }
  void OnMouseUp()
  {
      Object.transform.localPosition = originalLocalPosition;
      rabbitGame.Answer(isTrue);
  }
}
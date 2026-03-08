using UnityEngine;
using TMPro;

public class RabbitGameScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;

    private string currentQuestion;
    private bool currentAnswer;
    public AudioClip wrongSound;
    public AudioClip correctSound;
    public GameObject rabbit;
    public GameObject light;
    int correctCount = 0;
    public TextMeshPro scoreText;
    public TextMeshProUGUI scoreText2;
    public GameObject startPanel, gameOverPanel;
    bool isGameOver = false;

    private string[] questions = {
        "Eğer bir sayı çiftse 2’ye bölünür.",

        "Eğer bir sayı tekse 2’ye bölünür.",

        "Eğer kar yağıyorsa hava sıcaktır.",

        "Eğer güneş doğduysa hava aydınlanır.",

        "Eğer bir sayı 10’dan büyükse 5’ten büyüktür.",

        "Eğer bir sayı 4’e bölünüyorsa tek sayıdır.",

        "Eğer su 0°C’nin altındaysa donar.",

        "Eğer bir sayı 3'ten büyükse 5'den büyüktür.",

        "Eğer elektrik kesildiyse ışıklar yanmaz.",

        "Eğer bir sayı 5’e bölünüyorsa mutlaka çifttir."
    };

    private bool[] answers = {
        true,
        false,
        false,
        true,
        true,
        false,
        true,
        false,
        true,
        false,
    };

    private int questionIndex = 0;

    void Start()
    {
        LoadQuestion();
        // Başlangıçta beyaz ışık
    }

    void LoadQuestion()
    {
        currentQuestion = questions[questionIndex];
        currentAnswer = answers[questionIndex];
        questionText.text = currentQuestion;
    }

    public void Answer(bool isTrue)
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            return;
        }
        if (startPanel.activeSelf)
            return;
        else
        {
            if (isTrue == currentAnswer)
            {
                Debug.Log("Doğru!");
                AudioSource.PlayClipAtPoint(correctSound, transform.position, .2f);
                rabbit.GetComponentInChildren<Animator>().SetTrigger("Correct");
                light.GetComponent<Animator>().SetTrigger("Green");   // Doğru cevaba yeşil ışık
                correctCount++;
                scoreText.text = correctCount.ToString();  // Skoru güncelle
                scoreText2.text = "Toplanan Puan:" + correctCount.ToString();  // Skoru güncelle
            }
            else
            {
                Debug.Log("Yanlış!");
                AudioSource.PlayClipAtPoint(wrongSound, transform.position, .2f);
                rabbit.GetComponentInChildren<Animator>().SetTrigger("Wrong");
                light.GetComponent<Animator>().SetTrigger("Red");     // Yanlış cevaba kırmızı ışık
            }

            questionIndex++;
            if (questionIndex < questions.Length)
                LoadQuestion();
            else
            {
                questionText.text = "Oyun bitti!";
                isGameOver = true;
                return;
            }
        }
    }
}

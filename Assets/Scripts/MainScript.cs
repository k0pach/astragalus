using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Text rollCountText;  // Текстовое поле для количества бросков
    public Text scoreText;      // Текстовое поле для счета
    public Image[] diceImages;  // Массив изображений костей (5 шт.)
    public Sprite[] diceSprites; // 6 PNG-файлов (от 1 до 6)

    private int rollCount = 3; // Количество перебросов
    private int score = 0;     // Очки игрока
    private int[] diceValues = new int[5]; // Числовые значения выпавших костей

    public GameObject gameOverPanel;
    public GameObject gamePlayPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);
    }

    public void RollDice()
    {
        if (rollCount > 0)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                int diceResult = Random.Range(1, 7); // Генерируем число от 1 до 6
                diceValues[i] = diceResult; // Сохраняем значение кости
                diceImages[i].sprite = diceSprites[diceResult - 1]; // Меняем картинку
            }

            rollCount--; // Уменьшаем счетчик
            CalculateScore(); // Пересчитываем очки
            UpdateUI();
            CheckGameOver();
        }
    }

    public void StartGame()
    {
        rollCount = 3; 
        score = 0;
        UpdateUI();
        gamePlayPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    void UpdateUI()
    {
        rollCountText.text = rollCount + "/3"; 
        scoreText.text = score + "/300";
    }

    void CheckGameOver()
    {
        if (rollCount == 0 && score < 300)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ExitToMainMenu()
    {
        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 📌 Подсчет очков по комбинациям
    void CalculateScore()
    {
        int[] counts = new int[7]; // Массив для подсчета количества каждого числа (1-6)

        // Подсчитываем, сколько раз выпало каждое число
        foreach (int value in diceValues)
        {
            counts[value]++;
        }

        // Начисляем очки по правилам
        if (counts[1] == 3) score += 100;
        if (counts[1] == 4) score += 200;
        if (counts[1] == 5) score += 1000;
        
        if (counts[2] == 3) score += 20;
        if (counts[2] == 4) score += 40;
        if (counts[2] == 5) score += 200;

        if (counts[3] == 3) score += 30;
        if (counts[3] == 4) score += 60;
        if (counts[3] == 5) score += 300;

        if (counts[4] == 3) score += 40;
        if (counts[4] == 4) score += 80;
        if (counts[4] == 5) score += 400;

        if (counts[5] == 3) score += 50;
        if (counts[5] == 4) score += 100;
        if (counts[5] == 5) score += 500;

        if (counts[6] == 3) score += 60;
        if (counts[6] == 4) score += 120;
        if (counts[6] == 5) score += 600;
    }
}

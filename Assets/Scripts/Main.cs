using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using System;
using System.Collections;
using System.Security.Cryptography;
public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; }
    [SerializeField] private ShopManager shopManagerMoney;
    [SerializeField] private ShopManager shopManagerKnowledge;
    [SerializeField] private EventManager eventManager;

    public Animation Animation;
    public FallBooks FallBooks;

    [Header("Переменные валюты")]
    public double money, knowledge;
    [Header("UI элементы")]
    public TextMeshProUGUI MoneyText, KnowledgeText;
    [Header("Переменные накопления")]
    public float knowledgePerKlick = 1f, knowledgeMultiplier = 1f, knowledgePassive = 0f, moneyPassive = 0f;
    [Header("Эвенты")]
    public int minRandomTime = 10, maxRandomTime = 20;
    [Header("Звуки")]
    [SerializeField] private AudioSource background; // музыка 
    [SerializeField] private AudioSource buySound; // Звук покупки

    private void Awake()
    {
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем между сценами
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликаты
        }
        LoadGame();
    }


    private void Start()
    {
        StartCoroutine(PassiveEarn());
        StartCoroutine(EventRandom());
        //Запуск музыки
        background.Play();
    }


    private IEnumerator PassiveEarn()
    {
        while (true)
        {
            knowledge += knowledgePassive;
            money += moneyPassive;
            if (knowledgePassive > 0) {FallBooks.SpawnSprites(Convert.ToInt32(knowledgePassive));}
            yield return new WaitForSeconds(1f);
        }
    }


    private IEnumerator EventRandom()
    {
        while (true)
        {
            int randomtime = RandomNumberGenerator.GetInt32(minRandomTime, maxRandomTime + 1);
            yield return new WaitForSeconds(randomtime);
            if (!eventManager.IsEventAlreadyShown())
                eventManager.TriggerRandomEvent();
        }
    }


    void Update()
    {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) && (EventSystem.current.IsPointerOverGameObject() == false))
        {
            Animation.Play();
            FallBooks.SpawnSprites(Convert.ToInt32(knowledgePerKlick * knowledgeMultiplier));
            knowledge += Convert.ToInt64(knowledgePerKlick * knowledgeMultiplier);
        }

        KnowledgeText.text = knowledge.ToString();
        MoneyText.text = money.ToString();
    }

    // Запуск звука монетки
    public void GetMoneySound()
    {
        buySound.Play();
    }
    //Трата денег
    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }
    //Трата знаний
    public bool SpendKnowledge(int amount)
    {
        if (knowledge >= amount)
        {
            knowledge -= amount;
            return true;
        }
        return false;
    }

    //Накопление денег
    public void AddMoney(int amount)
    {
        money += amount;
    }

    //Накопление знаний
    public void AddKnowledge(long amount)
    {
        knowledge += amount;
    }

    // получение бонуса по его типу
    public void GetBonus(BonusType bonusType, float bonus)
    {
        switch (bonusType)
        {
            case BonusType.money:
                money += bonus;
                break;
            case BonusType.knowledgePassive:
                knowledgePassive += bonus;
                break;
            case BonusType.knowledgeMultiplier:
                knowledgeMultiplier += bonus;
                break;
            case BonusType.knowledgePerKlick:
                knowledgePerKlick += bonus;
                break;
            case BonusType.moneyPassive:
                moneyPassive += bonus;
                break;
            case BonusType.knowledge:
                knowledge += bonus;
                break;
        }
        GetMoneySound();
    }

    // Сохранение (Хз как проверить, попробуй если не впадлу будет)
    public void SaveGame()
    {
        // Сохраняем основные переменные
        PlayerPrefs.SetString("money", money.ToString());
        PlayerPrefs.SetString("knowledge", knowledge.ToString());
        PlayerPrefs.SetFloat("knowledgePerKlick", knowledgePerKlick);
        PlayerPrefs.SetFloat("knowledgeMultiplier", knowledgeMultiplier);
        PlayerPrefs.SetFloat("knowledgePassive", knowledgePassive);
        PlayerPrefs.SetFloat("moneyPassive", moneyPassive);
        if (shopManagerMoney != null)
            shopManagerMoney.SaveLevels("moneyShop");

        if (shopManagerKnowledge != null)
            shopManagerKnowledge.SaveLevels("knowledgeShop");

        PlayerPrefs.Save();
        Debug.Log("Игра сохранена через PlayerPrefs");
    }
    //Загрузка
    public void LoadGame()
    {
        // Загружаем основные переменные
        if (PlayerPrefs.HasKey("money"))
            money = Convert.ToDouble(PlayerPrefs.GetString("money"));

        if (PlayerPrefs.HasKey("knowledge"))
            knowledge = Convert.ToDouble(PlayerPrefs.GetString("knowledge"));

        knowledgePerKlick = PlayerPrefs.GetFloat("knowledgePerKlick", knowledgePerKlick);
        knowledgeMultiplier = PlayerPrefs.GetFloat("knowledgeMultiplier", knowledgeMultiplier);
        knowledgePassive = PlayerPrefs.GetFloat("knowledgePassive", knowledgePassive);
        moneyPassive = PlayerPrefs.GetFloat("moneyPassive", moneyPassive);

        if (shopManagerMoney != null)
            shopManagerMoney.LoadLevels("moneyShop");

        if (shopManagerKnowledge != null)
            shopManagerKnowledge.LoadLevels("knowledgeShop");

        Debug.Log("Игра загружена из PlayerPrefs");
    }

    //Сохра на закрытие
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //Сохра на сворачивание 
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
        else
        {
            LoadGame();
        }
    }
    
    //Cброс сохр
    public void ResetAllProgress()
{
    // Очищаем PlayerPrefs
    PlayerPrefs.DeleteAll();
    
    // Сбрасываем переменные к начальным значениям
    money = 0;
    knowledge = 0;
    knowledgePerKlick = 1f;
    knowledgeMultiplier = 1f;
    knowledgePassive = 0f;
    moneyPassive = 0f;

    // Сбрасываем уровни магазинов
    if (shopManagerMoney != null)
        shopManagerMoney.ResetLevels();
    
    if (shopManagerKnowledge != null)
        shopManagerKnowledge.ResetLevels();

    // Обновляем UI
    MoneyText.text = money.ToString();
    KnowledgeText.text = knowledge.ToString();

    Debug.Log("Весь прогресс сброшен!");
}

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using System;
using System.Collections;
public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; }
    private ShopManager shopManager;

    [Header("Переменные валюты")]
    public double money, knowledge;
    [Header("UI элементы")]
    public TextMeshProUGUI MoneyText, KnowledgeText;
    [Header("Переменные накопления")]
    public float knowledgePerKlick = 1f, knowledgeMultiplier = 1f, knowledgePassive = 0f, moneyPassive = 1f;

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
    }


    private void Start()
    {
        StartCoroutine(PassiveEarn());
    }


    private IEnumerator PassiveEarn()
    {
        while (true)
        {
            knowledge += knowledgePassive;
            money += moneyPassive;
            yield return new WaitForSeconds(1f);
        }
    }


    void Update()
    {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) && (EventSystem.current.IsPointerOverGameObject() == false))
        {
            knowledge += Convert.ToInt64(knowledgePerKlick * knowledgeMultiplier);
        }

        KnowledgeText.text = knowledge.ToString();
        MoneyText.text = money.ToString();
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

        // Сохраняем уровни улучшений из магазина
        if (shopManager != null)
        {
            for (int i = 0; i < shopManager.shopItems.Length; i++)
            {
                PlayerPrefs.SetInt("shopItemLevel_" + i, shopManager.GetCurrentLevel(i));
            }
        }

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
        moneyPassive = PlayerPrefs.GetFloat("moneyPassive", 1f);

        // Загружаем уровни улучшений из магазина
        if (shopManager != null)
        {
            for (int i = 0; i < shopManager.shopItems.Length; i++)
            {
                int level = PlayerPrefs.GetInt("shopItemLevel_" + i, 0);
                shopManager.currentLevels[i] = level;
            }
        }

        Debug.Log("Игра загружена из PlayerPrefs");
    }

}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopItem[] shopItems;

    // текущий уровень для каждого товара
    public int[] currentLevels { get; private set; }
    // Временные переменные, тебе нужно привязать к глобальным переменным валюты, силы клика и пас. дохода.

    private void Awake()
    {
        // Инициализируем массив, если он не создан
        if (currentLevels == null || currentLevels.Length != shopItems.Length)
        {
            currentLevels = new int[shopItems.Length];
            for (int i = 0; i < currentLevels.Length; i++)
                currentLevels[i] = 0;
        }
    }

    public void BuyNextLevel(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItems.Length) return;

        var item = shopItems[itemIndex];
        int level = currentLevels[itemIndex];

        // Проверка на достижение максимального уровня (только если товар не бесконечный)
        if (!item.isInfinite && level >= item.maxLevel)
        {
            Debug.Log("Максимальный уровень уже достигнут");
            return;
        }

        int price = item.GetPriceForLevel(level);
        float bonus = item.GetBonusForLevel(level);

        // Добавил проверку на достаточность денег перед покупкой + проверки на тип валюты
        if (item.GetCurrencyTypeForLevel(level) == CurrencyType.Knowledge)
        {
            if (!Main.Instance.SpendKnowledge(price))
            {
                Debug.Log("Недостаточно знаний");
                return;
            }
        }


        if (item.GetCurrencyTypeForLevel(level) == CurrencyType.Money)
        {
            if (!Main.Instance.SpendMoney(price))
            {
                Debug.Log("Недостаточно денег");
                return;
            }
        }
        BonusType bonusType = item.GetBonusTypeForLevel(level);
        Main.Instance.GetBonus(bonusType, bonus);


        Debug.Log($"Куплен {item.GetNameForLevel(level)} за {price}. Сила клика = {Main.Instance.knowledgePerKlick}, Доход = {Main.Instance.knowledgePassive}, Валюта = {Main.Instance.money}");

        if (!item.isInfinite)
            currentLevels[itemIndex]++;
    }


    // Метод для получения текущего уровня товара для UI
    public int GetCurrentLevel(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= currentLevels.Length) return 0;
        return currentLevels[itemIndex];
    }


        // Метод для сохранения уровней предметов
   public void SaveLevels(string saveKey)
    {
        if (currentLevels == null)
        {
            Debug.LogError("SaveLevels: currentLevels не инициализирован!");
            return;
        }

        for (int i = 0; i < currentLevels.Length; i++)
        {
            PlayerPrefs.SetInt($"{saveKey}_item_{i}", currentLevels[i]);
        }
    }


    // Метод для загрузки уровней предметов
    public void LoadLevels(string saveKey)
    {
        // Если массив не создан — создаем его
        if (currentLevels == null || currentLevels.Length != shopItems.Length)
        {
            currentLevels = new int[shopItems.Length];
        }

        // Проверяем, есть ли сохраненные данные
        bool hasSaveData = PlayerPrefs.HasKey($"{saveKey}_item_0");

        if (hasSaveData)
        {
            // Загружаем сохраненные уровни
            for (int i = 0; i < currentLevels.Length; i++)
            {
                currentLevels[i] = PlayerPrefs.GetInt($"{saveKey}_item_{i}", 0);
            }
        }
        else
        {
            // Если сохранений нет — сбрасываем в 0
            for (int i = 0; i < currentLevels.Length; i++)
            {
                currentLevels[i] = 0;
            }
        }
    }
    public void ResetLevels()
    {
    currentLevels = new int[shopItems.Length]; // Создаём новый массив с нулями
    for (int i = 0; i < currentLevels.Length; i++)
        currentLevels[i] = 0;
    }
}

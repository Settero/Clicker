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
        currentLevels = new int[shopItems.Length];
        for (int i = 0; i < currentLevels.Length; i++)
            currentLevels[i] = 0;
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
        int bonus = item.GetBonusForLevel(level);

        // Добавил проверку на достаточность денег перед покупкой!
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
}

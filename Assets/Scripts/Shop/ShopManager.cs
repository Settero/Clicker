using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopItem[] shopItems;

    // текущий уровень для каждого товара
    public int[] currentLevels { get; private set; }
    // Временные переменные, тебе нужно привязать к глобальным переменным валюты, силы клика и пас. дохода.
    public int clickPower = 1;      // сила клика
    public int moneyForMarket = 0;  // валюта
    public int passiveIncome = 0;   // пассивный доход

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
            Debug.Log("Недостаточно денег");
        }
        }


        if (item.GetCurrencyTypeForLevel(level) == CurrencyType.Money)
        {
            if (!Main.Instance.SpendMoney(price))
        {
            Debug.Log("Недостаточно денег");
        }
        }
        


        switch (item.bonusType)
        {
            case BonusType.ClickPower:
                clickPower += bonus;
                break;
            case BonusType.PassiveIncome:
                passiveIncome += bonus;
                break;
            case BonusType.MoneyInstant:
                moneyForMarket += bonus;
                break;
        }

        Debug.Log($"Куплен {item.GetNameForLevel(level)} за {price}. Сила клика = {clickPower}, Доход = {passiveIncome}, Валюта = {moneyForMarket}");

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

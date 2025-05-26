using UnityEngine;

// Бонусы которые дает товар
public enum BonusType
{
    knowledgePerKlick,    // Усиливает клик
    knowledgePassive, // Увеличивает доход в секунду
    money,  // Мгновенно добавляет деньги
    knowledge, // Мгновенно знания
    moneyPassive, // Деньги пассивно
    knowledgeMultiplier // Множитель клика
}

// Отдельный енум для типа валют
public enum CurrencyType
{
    Money,
    Knowledge
}

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string[] levelNames;         // Имена товара для каждого уровня
    public Sprite[] levelIcons;         // иконки для каждого уровня
    public int[] levelPrices;           // цены для каждого уровня
    public float[] levelBonuses;          // Бонус
    public string[] levelDescriptions;  // Описание для каждого уровня
    public BonusType bonusType;         // Тип бонуса

    public bool isInfinite;             // Можно ли бесконечно покупать товар
    public CurrencyType[] currencyTypes; // Валюта которой платить

    public int maxLevel => Mathf.Min(levelPrices.Length, levelBonuses.Length, levelIcons.Length);

    // Возвращает имя для указанного уровня
    public string GetNameForLevel(int level)
    {
        if (isInfinite) level = 0;
        if (level >= 0 && level < levelNames.Length)
            return levelNames[level];
        else
            return "Undefined";
    }

    // Возвращает иконку для указанного уровня.
    public Sprite GetIconForLevel(int level)
    {
        if (isInfinite) level = 0;
        if (level >= 0 && level < levelIcons.Length)
            return levelIcons[level];
        return null;
    }

    // Возвращает цену для указанного уровня.
    public int GetPriceForLevel(int level)
    {
        if (isInfinite) level = 0;
        if (level >= 0 && level < levelPrices.Length)
            return levelPrices[level];
        return 0;
    }

    // Возвращает бонус для указанного уровня.
    public float GetBonusForLevel(int level)
    {
        if (isInfinite) level = 0;
        if (level >= 0 && level < levelBonuses.Length)
            return levelBonuses[level];
        return 0;
    }

    // Возвращает описание товара для указанного уровня.
    public string GetDescriptionForLevel(int level)
    {
        if (level >= 0 && level < levelDescriptions.Length)
            return levelDescriptions[level];
        else
            return "";
    }

    // Возвращает тип валюты указанного уровня. (я хз что тут такое лвл. я так понял что как id используем)
    public CurrencyType GetCurrencyTypeForLevel(int level)
    {
        if (isInfinite) level = 0;
        if (level >= 0 && level < currencyTypes.Length)
            return currencyTypes[level];
        else
            return CurrencyType.Money;
    }
    
    // возвращает тип понуса
    public BonusType GetBonusTypeForLevel(int level)
    {
    return bonusType;
    }
}

using UnityEngine;

// Бонусы которые дает товар
public enum BonusType
{
    ClickPower,    // Усиливает клик
    PassiveIncome, // Увеличивает доход в секунду
    MoneyInstant   // Мгновенно добавляет деньги
}

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string[] levelNames;         // Имена товара для каждого уровня
    public Sprite[] levelIcons;         // иконки для каждого уровня
    public int[] levelPrices;           // цены для каждого уровня
    public int[] levelBonuses;          // Бонус
    public string[] levelDescriptions;  // Описание для каждого уровня
    public BonusType bonusType;         // Тип бонуса

    public bool isInfinite;             // Можно ли бесконечно покупать товар

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
    public int GetBonusForLevel(int level)
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

}

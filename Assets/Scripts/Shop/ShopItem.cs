using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string[] levelNames;              // базовое имя товара
    public Sprite[] levelIcons;         // иконки для каждого уровня
    public int[] levelPrices;           // цены для каждого уровня
    public int[] levelBonuses;          // Бонус
    public int maxLevel => Mathf.Min(levelPrices.Length, levelBonuses.Length, levelIcons.Length);

    // Возвращает имя для заданного уровня
    public string GetNameForLevel(int level)
    {
        if (level >= 0 && level < levelNames.Length)
            return levelNames[level];
        else
            return "Undefined";
    }

    //Возвращает иконку для заданного уровня
    public Sprite GetIconForLevel(int level)
    {
        if (level >= 0 && level < levelIcons.Length) return levelIcons[level];
        return null;
    }

    //Возвращает цену для заданного уровня
    public int GetPriceForLevel(int level)
    {
        if (level >= 0 && level < levelPrices.Length) return levelPrices[level];
        return 0;
    }

    //Возвращает бонус для заданного уровня
    public int GetBonusForLevel(int level)
    {
        if (level >= 0 && level < levelBonuses.Length) return levelBonuses[level];
        return 0;
    }
}

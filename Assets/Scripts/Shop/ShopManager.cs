using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopItem[] shopItems;

    private int[] currentLevels;  // текущий уровень для каждого товара
    private int n = 0;            // переменная, которую увеличивают бонусы

    private void Awake()
    {
        currentLevels = new int[shopItems.Length];
        for (int i = 0; i < currentLevels.Length; i++)
            currentLevels[i] = 0;  // стартуем с уровня 0
    }

    public void BuyNextLevel(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItems.Length) return;

        var item = shopItems[itemIndex];
        int level = currentLevels[itemIndex];

        if (level >= item.maxLevel)
        {
            Debug.Log("Максимальный уровень уже достигнут");
            return;
        }

        int price = item.GetPriceForLevel(level);
        int bonus = item.GetBonusForLevel(level);

        // Тут должна быть проверка на наличие денег (если есть)
        // Для примера просто увеличиваем n
        n += bonus;
        Debug.Log($"Куплен {item.GetNameForLevel(level)} за {price}. n = {n}");

        currentLevels[itemIndex]++;  // повышаем уровень

        // Тут можно вызвать обновление UI для товара itemIndex
    }

    // Метод для получения текущего уровня товара
    public int GetCurrentLevel(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= currentLevels.Length) return 0;
        return currentLevels[itemIndex];
    }
}

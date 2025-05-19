using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Добавь эту строку

public class ShopItemUI : MonoBehaviour
{
    public int itemIndex;
    public ShopManager shopManager;

    public TMP_Text nameText;   // Отображение имени товара
    public TMP_Text priceText;  // Отображение цены
    public TMP_Text bonusText;  // Отображение описания
    public Image iconImage;     // Иконка товара
    public Button buyButton;    // Кнопка покупки

    private void Start()
    {
        UpdateUI();
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    public void UpdateUI(int? overrideLevel = null)
    {
        int level = overrideLevel ?? shopManager.GetCurrentLevel(itemIndex);
        var item = shopManager.shopItems[itemIndex];

        bool isMax = !item.isInfinite && level >= item.maxLevel;

        if (isMax)
        {
            nameText.text = "Куплено";
            priceText.text = "-";
            bonusText.text = item.GetDescriptionForLevel(level);
            iconImage.sprite = item.GetIconForLevel(level - 1);
            buyButton.interactable = false;
        }
        else
        {
            nameText.text = item.GetNameForLevel(level);
            priceText.text = item.GetPriceForLevel(level).ToString();
            bonusText.text = item.GetDescriptionForLevel(level);
            iconImage.sprite = item.GetIconForLevel(level);
            buyButton.interactable = true;
        }
    }

    // Обработка нажатия на кнопку
    private void OnBuyButtonClick()
    {
        shopManager.BuyNextLevel(itemIndex);
        UpdateUI();
    }
}

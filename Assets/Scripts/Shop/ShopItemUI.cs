using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Добавь эту строку

public class ShopItemUI : MonoBehaviour
{
    public int itemIndex;
    public ShopManager shopManager;

    public TMP_Text nameText;    // заменили Text на TMP_Text
    public TMP_Text priceText;
    public TMP_Text bonusText;
    public Image iconImage;
    public Button buyButton;
    

    private void Start()
    {
        UpdateUI();
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void UpdateUI()
    {
        int level = shopManager.GetCurrentLevel(itemIndex);
        var item = shopManager.shopItems[itemIndex];

        if (level >= item.maxLevel)
        {
            nameText.text = "Куплено";
            priceText.text = "-";
            bonusText.text = "+" + item.GetBonusForLevel(level - 1) + " за клик";
            buyButton.interactable = false;
            iconImage.sprite = item.GetIconForLevel(level - 1);
        }
        else
        {
            nameText.text = item.GetNameForLevel(level);
            priceText.text = item.GetPriceForLevel(level).ToString();
            bonusText.text = "+" + item.GetBonusForLevel(level) + " за клик";
            buyButton.interactable = true;
            iconImage.sprite = item.GetIconForLevel(level);
        }
    }

    private void OnBuyButtonClick()
    {
        shopManager.BuyNextLevel(itemIndex);
        UpdateUI();
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventUI : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public Button centerButton;     // Для ивентов без выбора
    public Button leftChoiceButton; // Для первого варианта
    public Button rightChoiceButton;// Для второго варианта

    /// <summary>
    /// Обновляет UI для ивента без выбора.
    /// </summary>
    public void ShowEvent(GameEvent eventData)
    {
        titleText.text = eventData.eventName;
        descriptionText.text = eventData.description;

        centerButton.gameObject.SetActive(true);
        leftChoiceButton.gameObject.SetActive(false);
        rightChoiceButton.gameObject.SetActive(false);
    }

    /// Обновляет UI для ивента с выбором.
    public void ShowChoiceEvent(GameEvent eventData)
    {
        titleText.text = eventData.eventName;
        descriptionText.text = eventData.description;

        centerButton.gameObject.SetActive(false);
        leftChoiceButton.gameObject.SetActive(true);
        rightChoiceButton.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}

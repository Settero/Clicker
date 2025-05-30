using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("UI")]
    public EventUI eventUI;                     // Ссылка на UI ивента
    [Header("Event List")]
    public GameEvent[] availableEvents;         // Список возможных ивентов

    private GameEvent randomEvent;

    public Main main;
    int knowledge, money = 0;

    // Вызывается по кнопке
    public void TriggerRandomEvent()
    {
        if (availableEvents == null || availableEvents.Length == 0)
        {
            Debug.LogWarning("Нет доступных ивентов!");
            return;
        }

        // Выбор случайного ивента
        randomEvent = availableEvents[Random.Range(0, availableEvents.Length)];

        // Показываем ивент
        eventUI.gameObject.SetActive(true);
        if (randomEvent.hasChoice)
        {
            eventUI.ShowChoiceEvent(randomEvent);
        }
        else
        {
            eventUI.ShowEvent(randomEvent);
        }
    }

    public void ConfirmEvent()
    {
        if (randomEvent == null)
            return;

        // Проверяем второй тип бонуса
        if (randomEvent.Bonus2Type != GameEventBonusType.None)
        {
            // Ивент с выбором — применяем оба бонуса
            ApplyBonus(randomEvent.Bonus1Type, randomEvent.Bonus1Value);
            ApplyBonus(randomEvent.Bonus2Type, randomEvent.Bonus2Value);
            Debug.Log($"Принят ивент с выбором: {randomEvent.eventName} — {randomEvent.GetSingleBonusSummary()}, {randomEvent.GetChoice2Summary()}");
        }
        else
        {
            // Ивент без выбора
            ApplyBonus(randomEvent.Bonus1Type, randomEvent.Bonus1Value);
            Debug.Log($"Принят ивент без выбора: {randomEvent.eventName} — {randomEvent.GetSingleBonusSummary()}");
        }
    }

    // Универсальный обработчик бонусов
    private void ApplyBonus(GameEventBonusType type, int value)
    {
        switch (type)
        {
            case GameEventBonusType.Knowledge:
                if (main.knowledge + value < 0)
                {
                    main.knowledge = 0;
                }
                else
                {
                    main.AddKnowledge(value);
                }
                break;
            case GameEventBonusType.Money:
                if (main.money + value < 0)
                {
                    main.money = 0;
                }
                else
                {
                    main.AddMoney(value);
                }
            break;
            case GameEventBonusType.None:
                Debug.Log("Бонус отсутствует");
                break;
        }
    }

}

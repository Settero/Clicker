using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("UI")]
    public EventUI eventUI;                     // Ссылка на UI ивента
    [Header("Event List")]
    public GameEvent[] availableEvents;         // Список возможных ивентов

    private GameEvent randomEvent;

    public Main main;

    // Вызывается по кнопке
    public void TriggerRandomEvent()
    {
        bool isEventGood = true;
        if (availableEvents == null || availableEvents.Length == 0)
        {
            Debug.LogWarning("Нет доступных ивентов!");
            return;
        }

        // Выбор случайного ивента
        randomEvent = availableEvents[Random.Range(0, availableEvents.Length)];
        if (randomEvent.Bonus2Type != GameEventBonusType.None)
        {
            isEventGood = CheckRandomEvent(randomEvent.Bonus1Type, randomEvent.Bonus1Value);
            isEventGood = CheckRandomEvent(randomEvent.Bonus2Type, randomEvent.Bonus2Value);
        }
        else
        {
            // Ивент без выбора
            isEventGood = CheckRandomEvent(randomEvent.Bonus1Type, randomEvent.Bonus1Value);
        }
        if (isEventGood)
        {
            // Показываем ивент
            eventUI.gameObject.SetActive(true);
            if (randomEvent.hasChoice)
                eventUI.ShowChoiceEvent(randomEvent);
            else
                eventUI.ShowEvent(randomEvent);
        }
        else
            TriggerRandomEvent();
        
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
                main.AddKnowledge(value);
                break;
            case GameEventBonusType.Money:
                main.AddMoney(value);
                break;
            case GameEventBonusType.None:
                Debug.Log("Бонус отсутствует");
                break;
        }
    }


    //Проверка на наличие показываемого ивнта
    public bool IsEventAlreadyShown()
    {
        return eventUI != null && eventUI.gameObject.activeInHierarchy;
    }
    //Проверка на доступность
    public bool CheckRandomEvent(GameEventBonusType type, int value)
    {
        bool isEventGood = true;
        switch (type)
        {
            case GameEventBonusType.Knowledge:
                if (main.knowledge + value < 0)
                    isEventGood = false;
                break;
            case GameEventBonusType.Money:
                if (main.money + value < 0)
                    isEventGood = false;
                break;
            case GameEventBonusType.None:
                Debug.Log("Бонус отсутствует");
                break;
        }
        return isEventGood;
    }

}

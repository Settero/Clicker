using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("UI")]
    public EventUI eventUI;                     // Ссылка на UI ивента
    [Header("Event List")]
    public GameEvent[] availableEvents;         // Список возможных ивентов

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
        GameEvent randomEvent = availableEvents[Random.Range(0, availableEvents.Length)];

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

    // Закрыть UI ивента
    public void CloseEventUI()
    {
        eventUI.gameObject.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RandomEventGenerator : MonoBehaviour
{
    [SerializeField] float interval = 10f;
    [Range(0f, 1f)][SerializeField] float probability = .3f;

    [SerializeField] RandomEvent[] randomEvents;
    public RandomEventMessagesUI ui;

    void Start()
    {
        StartCoroutine(RandomChanceTrigger());
    }

    IEnumerator RandomChanceTrigger()
    {
        yield return new WaitForSeconds(interval);

        if (Random.value <= probability)
        {
            var randomEvent = randomEvents[Random.Range(0, randomEvents.Length)];
            randomEvent.Trigger();
            ui.AddMessage(randomEvent);

        }

        StartCoroutine(RandomChanceTrigger());
    }
}

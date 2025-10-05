using UnityEngine;
using UnityEngine.UI;

public class RandomEventMessagesUI : MonoBehaviour
{
    [SerializeField] LayoutGroup box;
    [SerializeField] RandomEventMessage message;

    public void AddMessage(RandomEvent randomEvent)
    {
        var msg = Instantiate(message, box.transform);
        msg.UpdateVisuals(randomEvent);
    }
}

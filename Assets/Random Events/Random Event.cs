using UnityEngine;

public abstract class RandomEvent : ScriptableObject
{
    public new string name;
    public string description;
    public abstract void Trigger();

    public void DisplayMessage()
    {
        FindAnyObjectByType<RandomEventMessagesUI>().AddMessage(this);
    }
}

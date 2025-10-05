using System.Collections;
using TMPro;
using UnityEngine;

public class RandomEventMessage : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5);
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    void Start()
    {
        StartCoroutine(Despawn());
    }

    public void UpdateVisuals(RandomEvent randomEvent)
    {
        title.text = randomEvent.name;
        description.text = randomEvent.description;
    }

    IEnumerator Despawn()
    {
        yield return _waitForSeconds5;
        Destroy(gameObject);
    }
}

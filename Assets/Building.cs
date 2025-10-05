using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Canvas uiToCreate;
    Canvas createdUi;

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy czy gracz wszed� w stref�
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController2D>().collidedBuilding = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController2D>().collidedBuilding = null;

        }
    }

    public void Enter()
    {
        createdUi = Instantiate(uiToCreate);
    }

    public void Leave()
    {
        Destroy(createdUi.gameObject);
    }
}

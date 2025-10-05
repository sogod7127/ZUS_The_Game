using UnityEngine;

public class ZUSTestTrigger : MonoBehaviour
{
    public EmployeeZUSContribution zusScript;

    [ContextMenu("Trigger ZUS Calculation")]
    public void TriggerNow()
    {
        if (zusScript == null)
        {
            Debug.LogError("Brak referencji do EmployeeZUSContribution!");
            return;
        }

        //zusScript.ComputeAndSave();
        Debug.Log("<color=green>[TEST]</color> Wywołano ComputeAndSave() ręcznie z testera!");
    }
}
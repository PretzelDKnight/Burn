using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class UIScript : MonoBehaviour ,ISelectHandler// required interface when using the OnSelect method.
{
    public MenuManager menuManager;

    public void OnSelect(BaseEventData eventData)
    {
        menuManager.Change();
    }
}

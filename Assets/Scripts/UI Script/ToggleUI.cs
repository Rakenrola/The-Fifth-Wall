using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleUI : MonoBehaviour
{
    public GameObject Panel;

    // Setup gameObj to disabled

    void Update()
    {
        // Close on ESC or "1"
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1)) && Panel != null && Panel.activeSelf)
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if(Panel != null || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1)) 
        //For convenience
        {
            Panel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void TogglePanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
}

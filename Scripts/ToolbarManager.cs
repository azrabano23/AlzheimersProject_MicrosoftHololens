using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarManager : MonoBehaviour
{

    public GameObject infoPopup;
    public GameObject settingsPopup;
    public GameObject familtyPortalPopup;

    public void ToggleInfoPopup()
    {
        Debug.Log("Toolbar Info Clicked");
        Toggle(infoPopup);
    }
    public void ToggleSettingsPopup()
    {
        Debug.Log("Toolbar Settings Clicked");
        Toggle(settingsPopup);
    }
    public void ToggleFamilyPortalPopup()
    {
        Debug.Log("Toolbar Family Portal Clicked");
        Toggle(familtyPortalPopup);
    }

    void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

}

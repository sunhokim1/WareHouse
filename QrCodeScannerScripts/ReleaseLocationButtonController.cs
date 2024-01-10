using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseLocationButtonController : MonoBehaviour
{
    public string region = "";
    public ReleaseManagement releaseManagement;
    public AGV_ControlTower agvCtr;
    public Button seoulBtn;
    public Button deajeonBtn;
    public Button busanBtn;
    public Button gwangjuBtn;
    public Button wonjuBtn;
    public Button releaseBtn;

    public void SeoulButton()
    {
        region = "seoul";
    }
    public void DeajeonButton()
    {
        region = "deajeon";
    }
    public void BusanButton()
    {
        region = "busan";
    }
    public void GwangjuButton()
    {
        region = "gwangju";
    }
    public void WonjuButton()
    {
        region = "wonju";
    }
    public void ReleaseButton()
    {
        if (region == "")
            return;
        ButtonFreeze();
        releaseManagement.ReleaseGoogleSheets(region);
    }
    public void ButtonFreeze()
    {
        seoulBtn.interactable = false;
        deajeonBtn.interactable = false;
        busanBtn.interactable = false;
        gwangjuBtn.interactable = false;
        wonjuBtn.interactable = false;
        releaseBtn.interactable = false;
    }
    public void ButtonMelt()
    {
        seoulBtn.interactable = true;
        deajeonBtn.interactable = true;
        busanBtn.interactable = true;
        gwangjuBtn.interactable = true;
        wonjuBtn.interactable = true;
        releaseBtn.interactable = true;
    }
}

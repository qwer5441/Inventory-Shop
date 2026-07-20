using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillPanel;
    private bool skillTreeOpen=false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                skillPanel.alpha = 0;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillPanel.alpha = 1;
                skillTreeOpen = true;
            }
        }

    }
}

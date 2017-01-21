using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{


    void Awake()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    public void OptionsButton()
    {
        if (gameObject.activeInHierarchy)
        {
            Close();
        }
        else
        {
            Open();
        }
    }


    void Open()
    {
        gameObject.SetActive(true);
        iTween.ScaleFrom(gameObject, iTween.Hash("y", 0.001f, "time", 0.5f, "easetype", iTween.EaseType.easeOutCirc));
    }

    void Close()
    {
        gameObject.SetActive(false);

    }



    public void ControlMode(int Option)
    {
        switch (Option)
        {
            case 0:
                InputManager.InputMode = InputManager.InputModes.FingerFollow;
                break;
            case 1:
                InputManager.InputMode = InputManager.InputModes.DrawCurrent;
                break;
        }

        GameManager.Instance.ConsoleWrite("Control mode: " + InputManager.InputMode.ToString());
    }
}

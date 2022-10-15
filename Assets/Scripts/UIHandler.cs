using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public string UI_playerName;

    public TMP_InputField nameInputField;

    public void SubmitButton()
    {
        UI_playerName = nameInputField.text;
        DataHandler.Instance.DHPlayername = UI_playerName;
    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

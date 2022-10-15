using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;
    public TMP_InputField nameInputField;

    public string playername;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetplayerName()
    {
        playername = nameInputField.text;
        Debug.Log(playername);
    }
}

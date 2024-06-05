using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    public Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        string name = PlayerPrefs.GetString("PlayerName", "NULL");
        Debug.Log("NameEdit Start: PlayerPrefsName: " + name);
        nameText.text = PlayerPrefs.GetString("PlayerName", "PLAYER");
        //Get a reference to the input field component
        InputField playerNameInput = GetComponent<InputField>();
        //Set the text of the input field to the saved player name
        playerNameInput.text = PlayerPrefs.GetString("PlayerName", "PLAYER");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerName(Text text )
    {
        string inputText = text.text;
        string playerName = "";
        Debug.Log("Input Text: " + inputText);
        string sanitizedText = Regex.Replace(inputText, @"[^a-zA-Z0-9]", "");

        // Only get the first 6 characters of the sanitized name
        if (sanitizedText.Length <= 1)
        {
            Debug.Log("Name too short");
            playerName = "PLAYA";
        }
        else if (sanitizedText.Length > 6)
        {
            Debug.Log("Trimming name");
            playerName = sanitizedText.Substring(0, 6); // Use Substring instead of Remove
        }
        else
        {
            playerName = sanitizedText;
        }

        nameText.text = playerName;
        PlayerPrefs.SetString("PlayerName", playerName);
        string name = PlayerPrefs.GetString("PlayerName", "NULL");
        Debug.Log("PlayerPrefsName: " + inputText);
        Debug.Log("PlayerName: " + playerName);

        //Get a reference to the input field component
        InputField playerNameInput = GetComponent<InputField>();
        //Set the text of the input field to the saved player name
        playerNameInput.text = PlayerPrefs.GetString("PlayerName", "PLAYER");
    }
}
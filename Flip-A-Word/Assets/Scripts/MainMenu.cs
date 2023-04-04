using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelName;
    
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var playButton = root.Q<Button>("Play");
        var quitButton = root.Q<Button>("Quit");

        playButton.clicked += PlayButton_OnClick;
        quitButton.clicked += QuitButton_OnClick;
    }

    void QuitButton_OnClick()
    {
        // Quit Application
        Application.Quit();
    }

    void PlayButton_OnClick()
    {
        //Load Level
        SceneManager.LoadScene(levelName);
    }
}

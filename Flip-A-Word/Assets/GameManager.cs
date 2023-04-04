using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField] OptionsController optionsController;
    [SerializeField] int correctAnswers = 0;
    [SerializeField] int inCorrectAnswers = 0;

    public event EventHandler<OnLevelLoadedEventArgs> OnLevelLoaded;
    public class OnLevelLoadedEventArgs : EventArgs
    {
        public Word word;
    }

    Word current_word;
    public static GameManager Instance { get; private set; }

    void Start()
    {
        Instance = this;

        optionsController.OnAnswerSelected += OptionsController_OnAnswerSelected;

        //Load Card
        LoadWord();
    }

    void OptionsController_OnAnswerSelected(object sender, OptionsController.OnAnswerSelectedEventArgs e)
    {
        if(e.word == current_word.word)
        {
            // Player chose correct option
            print("Player Chose Correct Option");
            LoadWord();
            correctAnswers++;
        }
        else
        {
            // Player chose incorrect option
            print("Player Chose Incorrect Option");
            LoadWord();
            inCorrectAnswers++;
        }
    }

    void LoadWord()
    {
        LoadWordFromList.LoadWord(ref current_word);
        Invoke("FireEvent", 1f);
    }

    void FireEvent()
    {
        OnLevelLoaded?.Invoke(this, new OnLevelLoadedEventArgs { word = current_word });
    }
}

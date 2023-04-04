using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

    public event EventHandler<OnLevelLoadedEventArgs> OnLevelLoaded;
    public class OnLevelLoadedEventArgs : EventArgs
    {
        public Word word;
    }

    public event EventHandler<OnScoreUpdatedEventArgs> OnScoreUpdated;
    public class OnScoreUpdatedEventArgs : EventArgs
    {
        public int updatedScore;
    }

    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] UIController uiController;

    [Header("Score Values")]
    [SerializeField] int AwardPoints = 100;
    [SerializeField] int DeductPoints = 50;

    int score;
    int currentWordIndex = 0;
    public int Score
    {
        get { return score; }
        set { score = value < 0 ? 0 : value; }
    }
    Word currentWord;



    private void Awake()
    {
        Instance = this;
        RequestWordFromAPI(LoadWord);
    }

    void Start()
    {  
        uiController.OnAnswerSelected += UIController_OnAnswerSelected;
    }

    void UIController_OnAnswerSelected(object sender, UIController.OnAnswerSelectedEventArgs e)
    {
        if(e.word == currentWord.word)
        {
            // Player chose correct option
            print("Player Chose Correct Option");
            LoadWord();
            Score+= AwardPoints;
        }
        else
        {
            // Player chose incorrect option
            print("Player Chose Incorrect Option");
            LoadWord();
            Score -= DeductPoints;
        }

        //Notify UI Controller to update UI
        OnScoreUpdated?.Invoke(this, new OnScoreUpdatedEventArgs { updatedScore = Score });
    }

    void LoadWord()
    {
        Debug.Log(currentWordIndex);
        if (currentWordIndex == WordsList.wordList.Count - 1)
        {
            // if last word is being played
            currentWord = WordsList.wordList[currentWordIndex];
            currentWordIndex = 0;
            RequestWordFromAPI(() => {});
            Invoke("FireEvent", 1f);
        }
        else
        {
            currentWord = WordsList.wordList[currentWordIndex];
            currentWordIndex++;
            Invoke("FireEvent", 1f);
        }
    }

    void FireEvent()
    {
        OnLevelLoaded?.Invoke(this, new OnLevelLoadedEventArgs { word = currentWord });
    }

    void RequestWordFromAPI(Action action)
    {
        LoadWordFromAPI.RequestRandomWord(10, 5, action);
    }
}

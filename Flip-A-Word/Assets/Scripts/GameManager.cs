using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

    public event EventHandler<OnNextWordLoadedEventArgs> OnNextWordLoaded;
    public class OnNextWordLoadedEventArgs : EventArgs
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

    [Header("Controls")]
    public bool isOnline = false;
    [Header("Online Controls")]
    [SerializeField] int letterCount = 6;
    [SerializeField] int wordsCount = 10;

    int score;
    int currentIndex;
    public int Score
    {
        get { return score; }
        set { score = value < 0 ? 0 : value; }
    }
    Word currentWord;



    void Awake()
    {
        Instance = this;
    }

    async void Start()
    {  
        uiController.OnAnswerSelected += UIController_OnAnswerSelected;
        Debug.Log("Online Check Ahead");
        if (isOnline)
            await APILoadWordList.RequestRandomWord(wordsCount, letterCount);

        LoadWord();
        Debug.Log("Got passed is online check");

    }

    void UIController_OnAnswerSelected(object sender, UIController.OnAnswerSelectedEventArgs e)
    {
        if(e.word == currentWord.word)
        {
            print("Player Chose Correct Option");
            Score+= AwardPoints;
        }
        else
        {
            print("Player Chose Incorrect Option");
            Score -= DeductPoints;
        }

        LoadWord();

        //Notify UI Controller to update UI
        OnScoreUpdated?.Invoke(this, new OnScoreUpdatedEventArgs { updatedScore = Score });
    }



    void LoadWord()
    {
        Debug.Log("Now we're in loadword");
        currentWord = LoadWordFromList.LoadWord(currentIndex, isOnline);
        if (currentWord != null)
            OnNextWordLoaded?.Invoke(this, new OnNextWordLoadedEventArgs { word = currentWord });
        else
            Debug.Log("Game WON!!!!");
    }
}

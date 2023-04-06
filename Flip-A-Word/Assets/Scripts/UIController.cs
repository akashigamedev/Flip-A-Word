using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public event EventHandler<OnAnswerSelectedEventArgs> OnAnswerSelected;
    public class OnAnswerSelectedEventArgs : EventArgs
    {
        public string word;
    }

    [SerializeField] GameObject loadingScreen;

    [Header("Text Elements")]
    [SerializeField] TMPro.TextMeshProUGUI wordText;
    [SerializeField] TMPro.TextMeshProUGUI definitionText;
    [SerializeField] TMPro.TextMeshProUGUI exampleText;
    [SerializeField] TMPro.TextMeshProUGUI option1Text;
    [SerializeField] TMPro.TextMeshProUGUI option2Text;
    [SerializeField] TMPro.TextMeshProUGUI option3Text;
    [SerializeField] TMPro.TextMeshProUGUI option4Text;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;


    [Header("Button Elements")]
    [SerializeField] Button optionBtn1;
    [SerializeField] Button optionBtn2;
    [SerializeField] Button optionBtn3;
    [SerializeField] Button optionBtn4;

    void Start()
    {
        GameManager.Instance.OnNextWordLoaded += GameManager_OnNextWordLoaded;
        GameManager.Instance.OnScoreUpdated += GameManager_OnScoreUpdated;

        optionBtn1.onClick.AddListener(() => HandleAnswerSelected(option1Text));
        optionBtn2.onClick.AddListener(() => HandleAnswerSelected(option2Text));
        optionBtn3.onClick.AddListener(() => HandleAnswerSelected(option3Text));
        optionBtn4.onClick.AddListener(() => HandleAnswerSelected(option4Text));
    }


    void HandleAnswerSelected(TMPro.TextMeshProUGUI optionText)
    {
        OnAnswerSelected?.Invoke(this, new OnAnswerSelectedEventArgs { word = optionText.text });
        optionBtn1.interactable = false;
        optionBtn2.interactable = false;
        optionBtn3.interactable = false;
        optionBtn4.interactable = false;
    }


    void GameManager_OnScoreUpdated(object sender, GameManager.OnScoreUpdatedEventArgs e)
    {
        scoreText.text = "Score: " + e.updatedScore;
    }

    void GameManager_OnNextWordLoaded(object sender, GameManager.OnNextWordLoadedEventArgs e)
    {
        print("In OnWordLoaded");
        loadingScreen.SetActive(false);
        // set card
        wordText.text = e.word.word;
        definitionText.text = e.word.definition;
        exampleText.text = e.word.example;

        List<Word> chosenWords = GetPossibleAnswers(e.word);

        option1Text.text = chosenWords[0].word;
        option2Text.text = chosenWords[1].word;
        option3Text.text = chosenWords[2].word;
        option4Text.text = chosenWords[3].word;


        optionBtn1.interactable = true;
        optionBtn2.interactable = true;
        optionBtn3.interactable = true;
        optionBtn4.interactable = true;

    }

    List<Word> GetPossibleAnswers(Word word)
    {

        // create a list to store chosen words with the correct word already in it
        List<Word> chosenWords = new List<Word>() { word };

        // randomly select three more options from wordlist
        LoadOptionsFromList.LoadOptions(ref chosenWords, GameManager.Instance.isOnline);

        #region ShuffleCode
        //Shuffle ChosenList to avoid spawning the correct choice at the same button every time
        System.Random rng = new System.Random();
        int n = chosenWords.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Word temp = chosenWords[k];
            chosenWords[k] = chosenWords[n];
            chosenWords[n] = temp;
        }
        #endregion

        return chosenWords;
    }
}

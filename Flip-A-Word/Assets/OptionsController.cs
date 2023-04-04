using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsController : MonoBehaviour
{
    public event EventHandler<OnAnswerSelectedEventArgs> OnAnswerSelected;
    public class OnAnswerSelectedEventArgs : EventArgs
    {
        public string word;
    }

    [SerializeField] UIDocument uiDocument; 

    Button Option1Btn, Option2Btn, Option3Btn, Option4Btn;
    List<Word> originalWordList = new List<Word>(WordsList.wordList);

    void Start()
    {
        GameManager.Instance.OnLevelLoaded += GameManager_OnLevelLoaded;

        var root = uiDocument.rootVisualElement;
        Option1Btn = root.Q<Button>("Option1");
        Option2Btn = root.Q<Button>("Option2");
        Option3Btn = root.Q<Button>("Option3");
        Option4Btn = root.Q<Button>("Option4");


        Option1Btn.clicked += () => HandleAnswerSelected(Option1Btn);
        Option2Btn.clicked += () => HandleAnswerSelected(Option2Btn);
        Option3Btn.clicked += () => HandleAnswerSelected(Option3Btn);
        Option4Btn.clicked += () => HandleAnswerSelected(Option4Btn);
    }

    void HandleAnswerSelected(Button btn)
    {
        OnAnswerSelected?.Invoke(this, new OnAnswerSelectedEventArgs { word = btn.text });
    }

    void GameManager_OnLevelLoaded(object sender, GameManager.OnLevelLoadedEventArgs e)
    {
        // create a list to store chosen words with the correct word already in it
        List<Word> chosenWords = new List<Word>() {e.word};

        // randomly select three more options from wordlist
        while (chosenWords.Count < 4)
        {
            int randomInt = UnityEngine.Random.Range(0, originalWordList.Count);
            if (!chosenWords.Contains(originalWordList[randomInt]))
                chosenWords.Add(originalWordList[randomInt]);
            else
                continue;
        }

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

        Option1Btn.text = chosenWords[0].word;
        Option2Btn.text = chosenWords[1].word;
        Option3Btn.text = chosenWords[2].word;
        Option4Btn.text = chosenWords[3].word;

    }
}

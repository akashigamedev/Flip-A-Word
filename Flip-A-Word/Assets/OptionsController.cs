using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsController : MonoBehaviour
{
    [SerializeField] LoadWordFromList loadWordFromList;
    [SerializeField] UIDocument uiDocument; 
    Button Option1Btn, Option2Btn, Option3Btn, Option4Btn;
    // Start is called before the first frame update
    void Start()
    {
        loadWordFromList.OnWordRecieved += LoadWordFromList_OnWordRecieved;

        var root = uiDocument.rootVisualElement;
        Option1Btn = root.Q<Button>("Option1");
        Option2Btn = root.Q<Button>("Option2");
        Option3Btn = root.Q<Button>("Option3");
        Option4Btn = root.Q<Button>("Option4");
    }

    void LoadWordFromList_OnWordRecieved(object sender, LoadWordFromList.OnWordsRecievedEventArgs e)
    {
        // create a list to store chosen words with the correct word already in it
        List<Word> chosenWords = new List<Word>() {e.word};


        // randomly select three more options from wordlist
        while (chosenWords.Count < 4)
        {
            int randomInt = UnityEngine.Random.Range(0, WordsList.wordList.Count);
            if (!chosenWords.Contains(WordsList.wordList[randomInt]))
                chosenWords.Add(WordsList.wordList[randomInt]);
            else
                continue;
        }

        #region ShuffleCode
        //Shuffle ChosenList to avoid spawning the correct choice at the same button every time
        System.Random rng = new System.Random();
        int n = chosenWords.Count;

        while(n > 1)
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

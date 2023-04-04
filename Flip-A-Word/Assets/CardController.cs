using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] LoadWordFromList loadWordsFromList;
    [SerializeField] UIDocument uiDocument;

    private void Start()
    {
        loadWordsFromList.OnWordRecieved += LoadWordsFromList_OnWordRecieved;
    }

    void LoadWordsFromList_OnWordRecieved(object sender, LoadWordFromList.OnWordsRecievedEventArgs e)
    {
        var root = uiDocument.rootVisualElement;
        var word = root.Q<Label>("Word");
        var definition = root.Q<Label>("Definition");
        var example = root.Q<Label>("Example");

        word.text = e.word.word;
        definition.text = e.word.definition;
        example.text = e.word.example;
    }
}

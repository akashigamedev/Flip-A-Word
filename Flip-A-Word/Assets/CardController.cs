using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;

    Label word, definition, example;

    void Start()
    {
        GameManager.Instance.OnLevelLoaded += GameManager_OnLevelLoaded;

        var root = uiDocument.rootVisualElement;
        word = root.Q<Label>("Word");
        definition = root.Q<Label>("Definition");
        example = root.Q<Label>("Example");
    }

    void GameManager_OnLevelLoaded(object sender, GameManager.OnLevelLoadedEventArgs e)
    {
        // Loading Card
        word.text = e.word.word;
        definition.text = e.word.definition;
        example.text = e.word.example;
    }
}

using System;
using UnityEngine;

public static class LoadWordFromList
{
    public static void LoadWord(ref Word word)
    {
        if (WordsList.wordList.Count <= 0)
        {
            Debug.LogError("WordsList is empty! Player Won!!");
            return;
        }
         
        int randomInt = UnityEngine.Random.Range(0, WordsList.wordList.Count);
        word =  WordsList.wordList[randomInt];
        WordsList.wordList.RemoveAt(randomInt);
    }
}

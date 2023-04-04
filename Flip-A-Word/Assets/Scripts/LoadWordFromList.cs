using System;
using UnityEngine;

public static class LoadWordFromList
{
    public static void LoadWord(ref Word word)
    {
        if (WordsList.offlineWordList.Count <= 0)
        {
            Debug.LogError("WordsList is empty! Player Won!!");
            return;
        }
         
        int randomInt = UnityEngine.Random.Range(0, WordsList.offlineWordList.Count);
        word =  WordsList.offlineWordList[randomInt];
        WordsList.offlineWordList.RemoveAt(randomInt);
    }
}

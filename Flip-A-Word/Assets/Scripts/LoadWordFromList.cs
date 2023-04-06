using System;
using UnityEngine;

public static class LoadWordFromList
{
    public static Word LoadWord(int currentIndex, bool isOnline)
    {
        switch(isOnline)
        {
            case true:
                    if (currentIndex == WordsList.onlineWordList.Count - 1)
                        return null;

                    int randomInt = UnityEngine.Random.Range(0, WordsList.onlineWordList.Count);
                    Word word = WordsList.onlineWordList[randomInt];
                    return word;
            default:
            case false:
                if (currentIndex == WordsList.offlineWordList.Count - 1)
                    return null;

                int rInt = UnityEngine.Random.Range(0, WordsList.offlineWordList.Count);
                Word newWord = WordsList.offlineWordList[rInt];
                return newWord;
        }
    }
}

using System;
using UnityEngine;

public class LoadWordFromList : MonoBehaviour
{
    public event EventHandler<OnWordsRecievedEventArgs> OnWordRecieved;
    public class OnWordsRecievedEventArgs : EventArgs
    {
        public Word word;
    }
    void Start()
    {
        int randomInt = UnityEngine.Random.Range(0, WordsList.wordList.Count);
        Word _word = WordsList.wordList[randomInt];

        OnWordRecieved?.Invoke(this, new OnWordsRecievedEventArgs
        {
            word = _word
        });
    }
}

using System.Collections.Generic;

public static class LoadOptionsFromList
{
    public static void LoadOptions(ref List<Word> list, bool isOnline)
    {
        switch(isOnline)
        {
            case true:
                while (list.Count < 4)
                {
                    int randomInt = UnityEngine.Random.Range(0, WordsList.onlineWordList.Count);
                    if (!list.Contains(WordsList.onlineWordList[randomInt]))
                        list.Add(WordsList.onlineWordList[randomInt]);
                    else
                        continue;
                }
                break;

            case false:
                while(list.Count < 4)
                {
                    int randomInt = UnityEngine.Random.Range(0, WordsList.offlineWordList.Count);
                    if (!list.Contains(WordsList.offlineWordList[randomInt]))
                        list.Add(WordsList.offlineWordList[randomInt]);
                    else
                        continue;
                }
                break;
        }
    }
}

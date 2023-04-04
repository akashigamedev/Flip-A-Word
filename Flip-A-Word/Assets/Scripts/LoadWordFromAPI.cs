using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public static class LoadWordFromAPI
{
    static readonly object lockObj = new object();

    public static async void RequestRandomWord(int wordCount, int letterCount, Action action)
    {
        string request = $"https://random-word-api.herokuapp.com/word?number={wordCount}&length={letterCount}";


        HttpClient client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(request);

        string responseContent = await response.Content.ReadAsStringAsync();
        string[] words = JsonConvert.DeserializeObject<string[]>(responseContent);

        client.Dispose();

        ConcurrentBag<Word> wordList = new ConcurrentBag<Word>();

        var tasks = new List<Task>();
        foreach(var word in words)
        {
            tasks.Add(Task.Run(async () =>
            {
                Word wordObj = await RequestWordDefinition(word);
                if(wordObj != null)
                {
                    wordList.Add(wordObj);
                }

            }));
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

        // Assign New WordList For Game To Use
        WordsList.wordList = new List<Word>(wordList);

        action();
    }

    static async Task<Word> RequestWordDefinition(string word)
    {
        
        string dictionaryRequest = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(dictionaryRequest);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JArray jArray = JArray.Parse(json);

                    foreach (JObject result in jArray)
                    {
                        JArray meanings = (JArray)result["meanings"];

                        foreach (JObject meaning in meanings)
                        {
                            JArray definitionsArray = (JArray)meaning["definitions"];

                            if (definitionsArray.Count > 0)
                            {
                                string def = (string)definitionsArray[0]["definition"];

                                lock (lockObj)
                                {
                                    return new Word(word, def, "No Example Found");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log(response.StatusCode);
                }
            }
        }
        catch (HttpRequestException e)
        {
            Debug.Log($"Error: {e.Message}");
        }

        return null;

    }
}

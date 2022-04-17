using System;
using System.Collections.Generic;

namespace How_many_sentences
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] words = new string[] { "bats", "tabs", "bats", "in", "cat", "act"};
      string[] sentences = new string[] { "cat act the bats", "in the act", "act tabs in" };
      Program p = new Program();
      var result = p.HowManySentences(words, sentences);
      Console.WriteLine(string.Join(",", result));
    }

    // https://leetcode.com/discuss/interview-question/1541093/How-many-sentences-Can-someone-provide-a-python-solution-for-this-question
    // Given an array of words and an array of sentences, determine which words are anagrams of each other.
    // Calculate how many sentences can be created by replacing any word with one of its anagrams,
    // Example wordSet = ['listen' 'silent, 'it', 'is'] sentence = "listen it is silent"
    // Determine that listen is an anagram of silent. Those two words can be replaced with their anagrams.
    // The four sentences that can be created are:
    // • listen it is silent
    // • listen it is listen
    // • silent it is silent
    // • silent it is listen​
    public int[] HowManySentences(string[] words, string[] sentences)
    {
      // use to create the group anagrams
      Dictionary<string, HashSet<string>> wordMap = new Dictionary<string, HashSet<string>>();
      // use to get all teh anagrams for the passed input words
      Dictionary<string, HashSet<string>> wordMapInUse = new Dictionary<string, HashSet<string>>();
      // first create the group anagram
      foreach (string word in words)
      {
        // sort each word
        var sortedWord = Sort(word);
        if (!wordMap.ContainsKey(sortedWord))
        {
          wordMap[sortedWord] = new HashSet<string>();
        }
        if (!wordMapInUse.ContainsKey(word))
        {
          wordMapInUse[word] = new HashSet<string>();
        }
        var existingAnagrams = wordMap[sortedWord];
        existingAnagrams.Add(word);
        wordMap[sortedWord] = existingAnagrams;

        wordMapInUse[word] = existingAnagrams;
      }
      var result = new int[sentences.Length];
      int index = 0;
      foreach (string sentence in sentences)
      {
        int c = 1;
        var wordsInSentence = sentence.Trim().Split(" ");
        foreach (string ws in wordsInSentence)
        {
          if (wordMapInUse.ContainsKey(ws))
          {
            var existing = wordMapInUse[ws];
            c *= existing.Count;
          }
        }
        result[index] = c;
        index++;
      }

      return result;
    }

    private string Sort(string str)
    {
      var strArray = str.ToCharArray();
      Array.Sort(strArray);
      var sortedStr = new string(strArray);
      return sortedStr;
    }
  }
}

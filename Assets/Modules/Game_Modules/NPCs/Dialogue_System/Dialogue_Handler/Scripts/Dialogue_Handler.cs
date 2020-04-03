using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Handler : MonoBehaviour
{
    [SerializeField]
    private TextAsset diaglogueText;
    private Text diagText;

    private string[] _lines;
    List<int> dialogueIndexes = new List<int>();
    int _lineIndex = 0;
    bool isReading = true;

    private void Start()
    {
        diagText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        _lines = Regex.Split(diaglogueText.text, "\n");
        _lines = _lines.Where(x => !string.IsNullOrEmpty(x.TrimStart(' '))).ToArray();

        //StartCoroutine(SearchText());
        //DialogueSetup();
    }

    void DialogueSetup()
    {

        //for (_lineIndex = 0; _lineIndex < _lines.Length; _lineIndex++)
        //if (!_lines[_lineIndex].StartsWith("#M") && _lines[_lineIndex].Length > 2 && isReading)
        //TagSwitch(_lines[_lineIndex]);
        //for (int i = 0; i < dialogueIndexes.Count; i++)
        //{
        //Debug.Log(_lines[dialogueIndexes[i]] + " : " + dialogueIndexes[i]);
        //}
    }

    /*
    IEnumerator SearchText()
    {
        while (isReading)
        {
            if (_lineIndex < _lines.Length)
            {
                _lineIndex++;
                if (!_lines[_lineIndex].StartsWith("#M") && _lines[_lineIndex].Length > 2)
                    TagSwitch(_lines[_lineIndex]);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    */

    void TagSwitch(string input)
    {

        string tagTest = input.Substring(0, 2);
        //Debug.Log(input.Length + " : " + tagTest + " : " + input);
        switch (tagTest)
        {
            case "#D":
                dialogueIndexes.Add(_lineIndex);
                //Debug.Log(tagTest);
                break;
            case "#N":
                //Debug.Log(tagTest);
                break;
            case "#C":
                //Debug.Log(tagTest);
                break;
            case "#R":
                //Debug.Log(tagTest);
                break;
            case "#E":
                //Debug.Log(tagTest);
                break;
        }
    }


}


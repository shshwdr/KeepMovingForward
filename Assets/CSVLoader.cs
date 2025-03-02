using System.Collections;
using System.Collections.Generic;
using Sinbad;
using UnityEngine;

public class DialogueInfo
{
    public string id;
    public string text;
    public string speaker;
    public int type;
    public string otherEvent;
    public string next;
    public string respond;
    public string respondCheck;

}

public class CSVLoader : Singleton<CSVLoader>
{
    public Dictionary<string, List<DialogueInfo>> DialogueInfoMap = new Dictionary<string, List<DialogueInfo>>();
    public Dictionary<string, int> dialogueIndex = new Dictionary<string, int>();

    public void Init()
    {
        var heroInfos =
            CsvUtil.LoadObjects<DialogueInfo>(GetFileNameWithABTest("dialogue"));
        foreach (var info in heroInfos)
        {
            if (!DialogueInfoMap.ContainsKey(info.id))
            {
                DialogueInfoMap.Add(info.id, new List<DialogueInfo>());
            }
            DialogueInfoMap[info.id].Add(info);
            dialogueIndex[info.id] = 0;
        }
    }
    
    string GetFileNameWithABTest(string name)
    {
        // if (ABTestManager.Instance.testVersion != 0)
        // {
        //     var newName = $"{name}_{ABTestManager.Instance.testVersion}";
        //     //check if file in resource exist
        //      
        //     var file = Resources.Load<TextAsset>("csv/" + newName);
        //     if (file)
        //     {
        //         return newName;
        //     }
        // }
        return name;
    }
}

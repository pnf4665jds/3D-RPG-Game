using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    private string NPCname;
    private List<string> contentToTalk = new List<string>();

    public NPC(string name) {
        this.NPCname = name;
    }
    public void changeName(string name) {
        this.NPCname = name;
    }
    public void addDialog(List<string> dialog) {

        contentToTalk.AddRange(dialog);

    }
    public string getDialog(int index) {
        return contentToTalk[index];
    }
    public string getName() {
        return NPCname;
    }
}

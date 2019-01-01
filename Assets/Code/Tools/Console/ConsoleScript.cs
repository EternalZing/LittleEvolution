using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour {
    public delegate object ConsoleFunction(List<string> arg);

    [SerializeField]
    private Dictionary<string, ConsoleFunction> _consoleFunctions = new Dictionary<string, ConsoleFunction>();

    private bool _enable = true;

    private int _ingnoreCount = 1;
    private int _logIngore = 10;
    private string _logString = "";

    public InputField ConsoleInput;

    // Use this for initialization
    public Text ConsoleLog;

    private void Start() {
        _consoleFunctions.Add("clear", ConsoleClear);
        _consoleFunctions.Add("sets",SetObjectStatus);
    }

    private object ConsoleClear(List<string> arg) {
        if (_enable == false) return null;
        _logString = "";
        return "Clear Success";
    }

    private object SetObjectStatus(List<string> arg) {
        if (_enable == false) return null;
        foreach (var v in arg) {
            Debug.Log(arg);
        }
        GameObject target=  GameObject.Find(arg[0]);
        char[] vv = arg[1].ToCharArray();
        vv[0] = Char.ToUpper(vv[0]);
        //target.GetComponent<BaseCharacter>().MeCharacterStatus.SetValue(new string(vv), arg[2]);
        return null;
    }
    private List<string> historyCommand= new List<string>();
    public void CommandConsole() {
        if (_enable == false) return;
        var input = ConsoleInput.text;
        Log(input);
        
        if (historyCommand.Count>0&&!historyCommand[historyCommand.Count - 1].Equals(input)) {
            historyCommand.Add(input);
        }
        
        if (input.Trim().Equals("")) {
            Log("Empty Command");
            return;
        }

        var arg = input.Split(' ');
        Log(input);
        arg[0] = arg[0].ToLower();
        if (_consoleFunctions.ContainsKey(arg[0]))
            _consoleFunctions[arg[0]](new List<string>(arg).GetRange(1, arg.Length - 1));
        ConsoleInput.text = "";
        ConsoleInput.ActivateInputField();
        commandRoller = historyCommand.Count - 1;
    }

    public void SetLogSensibility(int f) {
        _logIngore = f;
        _ingnoreCount = _logIngore;
    }

    //Normal Log
    public void Log(params object[] list) {
        if (_enable == false || LogIngore()) return;
        foreach (var o in list) _logString = _logString + "\n" + o + "\t";
    }

    private bool LogIngore() {
        _ingnoreCount--;
        if (_ingnoreCount == 0) {
            _ingnoreCount = _logIngore;
            return false;
        }

        return true;
    }

    private bool NextOutput() {
        if (_ingnoreCount == 1)
            return true;
        return false;
    }

    //Hard Log
    public void LogH(params object[] list) {
        if (_enable == false) return;
        foreach (var o in list) _logString = _logString + "\n" + o + "\t";
    }

    public void LogOnly(params object[] list) {
        if (_enable == false || !NextOutput()) return;
        _logString = "";
        Log(list);
    }
    public void LogWatch(params object[] list) {
    }
    private int commandRoller=0;
    private void Update() {
        if (_enable == false) return;
        if (ConsoleLog != null)
            ConsoleLog.text = _logString;
        else
            Debug.Log("Unable to Start internal Console, Can't target Text Block");
        
        if (Input.GetKeyDown(KeyCode.Return)) CommandConsole();
        //上下键获取历史信息
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log(KeyCode.UpArrow);
            if (commandRoller > 0) {
                commandRoller = commandRoller - 1;
                ConsoleCaller.Log(commandRoller);
                ConsoleInput.text = historyCommand[commandRoller];
            }   
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (commandRoller < historyCommand.Count-1) {
                commandRoller = commandRoller + 1;
                ConsoleInput.text = historyCommand[commandRoller];
            }   
        }
    }
    public void CallConsole() {
        _enable = true;
    }
    public void CloseConsole() {
        _enable = false;
    }
}
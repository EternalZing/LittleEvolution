using UnityEngine;

public class ConsoleCaller : MonoBehaviour {
    private static ConsoleScript globalConsole;

    public GameObject console;

    // Use this for initialization
    private void Start() {
        globalConsole = console.GetComponent<ConsoleScript>();
        Log("Console Start");
        SetSensibility(15);
    }

    public static void Log(params object[] list) {
        globalConsole.Log(list);
    }


    public static void LogOnly(params object[] list) {
        globalConsole.LogOnly(list);
    }

    public static void SetSensibility(int f) {
        globalConsole.SetLogSensibility(f);
    }

    private void Update() {
        if (console == null) return;
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            if (console.active) {
                console.GetComponent<ConsoleScript>().CloseConsole();
                console.SetActive(false);
            }
            else {
                console.SetActive(true);
                console.GetComponent<ConsoleScript>().CallConsole();
            }
        }
    }
}
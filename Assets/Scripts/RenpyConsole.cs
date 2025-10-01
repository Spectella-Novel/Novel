using System.Collections.Generic;
using UnityEngine;
using RenDisco;
using System;
using Assembly_CSharp;
using System.Threading;
using RenDisco.Implementation;
using System.Threading.Tasks;
using Implementation;
using Novel.Managers;
using System.Collections;
using Novel.Loader;

public class RenpyConsole : MonoBehaviour
{
    private AsyncStepper _stepper;
    [SerializeField] private DialogueManager dialogue;

    // Start is called before the first frame update
    async void Start()
    {
        System.Console.SetOut(new UnityTextWriter());;
        string script = @"
            define e = Character(""e"")
            label start:
                e ""Hello, world!""
                jump start_2

            label start_2:
                e ""start_2, world!""
                show ""Image"" a
                # Варианты ответа
                menu:
                    ""Попробовать подбодрить Анну"":
                        jump start_3

                    ""Спросить, что случилось"":
                        jump start_3

                    ""Остаться равнодушным"":
                        jump start_3
        
            label start_3:
                ""narrative, world!""
                jump start_4
            label start_4:
                e ""start_4, world!""
                jump start_5
            label start_5:
                e ""start_5, world!""
                jump finish
            label finish:
                e ""Goodbye, world!""
                return
            ";
        IRenpyParser parser = new AntlrRenpyParser();
        List<Instruction> commands = parser.Parse(script);
        SignalBroker.Initialize();
        SignalBroker.On(DefaultSignals.Choice, OnInputHandler);
        SignalBroker.On(DefaultSignals.AnyAction, OnAnyAction);
        FileLoader.Init();
        var storage = new UnityStorage();
        
        var factory = new UnityCommandFactory(dialogue, storage);
        
        _stepper = new AsyncStepper(commands, factory);
        _stepper.Start();

    }
    void OnAnyAction(string signal)
    {
        StartCoroutine(WaitForAnyAction());
    }

    IEnumerator WaitForAnyAction()
    {
        while (!Input.anyKey) {
            yield return null;
        }
        WaitableMessageBroker.Publish(DefaultSignals.AnyAction, null);

    }

    void OnInputHandler(string signal)
    {
        WaitableMessageBroker.Publish(DefaultSignals.Choice, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }

}

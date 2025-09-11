using System.Collections.Generic;
using UnityEngine;
using RenDisco;
using System;
using Assembly_CSharp;
using System.Threading;
using RenDisco.Implementation;
using System.Threading.Tasks;
using Implementation;

public class RenpyConsole : MonoBehaviour
{
    private Game game;
    private AsyncStepper _stepper;
    private Action Delegate;
    [SerializeField] private DialogueComponent dialogue;

    // Start is called before the first frame update
    async void Start()
    {
        System.Console.SetOut(new UnityTextWriter());
        Console.WriteLine("Test");
        string script = @"
            define e = Character(""e"")
            label start:
                e ""Hello, world!""
                jump start_2

            label start_2:
                e ""start_2, world!""
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
        var storage = new UnityStorage();
        var factory = new UnityCommandFactory(dialogue, storage);
        _stepper = new AsyncStepper(commands, factory);
        Console.WriteLine("start");

        _stepper.Start();
        Delegate = OnInputHandler;
        SignalBroker.On("Input", Delegate);
    }
    void OnInputHandler()
    {
        WaitableMessageBroker.Publish("Input", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

}

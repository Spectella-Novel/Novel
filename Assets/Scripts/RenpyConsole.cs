using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenDisco;
using System;
using System.IO;
using Assembly_CSharp;
using NovelEngine;
public class RenpyConsole : MonoBehaviour
{
    UnityRuntimeEngine engine;
    // Start is called before the first frame update
    void Start()
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
                jump finish
            label finish:
                e ""Goodbye, world!""
                return
            ";
        IRenpyParser parser = new AntlrRenpyParser();
        List<Command> commands = parser.Parse(script);
        IRuntimeEngine runtime = new UnityRuntimeEngine();
        Game game = new Game(runtime, commands);
        var counter = 0;
        while(game.gameIsRunning && counter < 10)
        {
            counter++;
            // Check if we need to read a choice from the user
            if (game.WaitingForInput)
            {
                Debug.Log("> 1 // нет ввода");
                var userChoice = 1;

                // Create a StepContext with the user's choice loaded
                InputContext inputContext = new InputContext(userChoice - 1);
                game.Step(inputContext: inputContext);
            }
            else
            {
                Debug.Log("-");
                game.Step();
            }
        }
        Debug.Log(counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

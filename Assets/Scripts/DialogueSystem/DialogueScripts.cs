using DialogueSystem;
using DialogueSystem.Characters;
using DialogueSystem.Enums;
using DialogueSystem.Nodes.Dialogue;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScripts : MonoBehaviour
{
    public DialogueGraph DSGraph;

    public TMP_Text text;

    public Transform buttonContainer; // Контейнер для кнопок

    public Button prefabButton;

    public int numberOfButtons = 5; // Количество кнопок для генерации

    public DialogueNodeBase CurrentNode;

    public RelativeAnchor anchor;

    private Action mouseDown;



    private void Start()
    {
        DSGraph.Init();
        var entryPoint = DSGraph.EntryPoint;
        var port = entryPoint.GetPort(nameof(entryPoint.Start));
        CurrentNode = port.Connection.node as DialogueNodeBase;
        Init();

    }

    private void GenerateAnsweredDialogue(AnsweredDialogueNode dialogueNode)
    {
        text.text = dialogueNode.Text;

        for (int i = 0; i < dialogueNode.Answers.Count; i++)
        {
            var answer = dialogueNode.Answers[i];

            Button newButton = Instantiate(prefabButton, buttonContainer);
            newButton.gameObject.SetActive(true); // Убедитесь, что кнопка активна
            newButton.transform.localScale = Vector3.one;
            // Установите текст кнопки
            newButton.GetComponentInChildren<TMP_Text>().text = answer;

            // Добавьте обработчик события нажатия на кнопку
            int buttonIndex = i; // Локальная переменная для замыкания
            newButton.onClick.AddListener(() => OnButtonClick(dialogueNode, buttonIndex));
        }
        void OnButtonClick(AnsweredDialogueNode answeredDialogue, int index)
        {
            // Получаем узел
            CurrentNode = CurrentNode.GetNextDialogue(index);

            if (CurrentNode == null)
            {
                UnityEngine.Application.Quit();
                return;
            }

            Init();
        }
    }


    private void Init()
    {
        var composition =  CurrentNode.Get<Composition>(NovelTypes.Prefab.Characters);

        foreach (var position in composition?.Positions)
        {
            var a = anchor.Anchors.Find(anchor => anchor.Key == ((Character.RelativePosition)position).Position);
            var character = composition.Get(position);
            var go = a.Value;

            // Add an Image component
            Image image = go.GetComponent<Image>();
            image ??= go.AddComponent<Image>();
            image.sprite = character.CurrentSprite;
        }
        // Удаляем все дочерние элементы из контейнера
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        switch (CurrentNode)
        {
            case UnansweredDialogueNode unansweredDialogue:
                GenerateUnansweredDialogue(unansweredDialogue);
                break;
            case AnsweredDialogueNode answeredDialogue:
                GenerateAnsweredDialogue(answeredDialogue);
                break;
        }
    }

    private void GenerateUnansweredDialogue(UnansweredDialogueNode answeredDialogue)
    {
        text.text = answeredDialogue.Text;
        mouseDown += OnMouseDown;
        void OnMouseDown()
        {
            CurrentNode = CurrentNode.GetNextDialogue();
            mouseDown -= OnMouseDown;
            Init();
        }
    }



    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseDown?.Invoke();
        }
    }

}

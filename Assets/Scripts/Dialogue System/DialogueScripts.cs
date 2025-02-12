using DialogueSystem;
using DialogueSystem.Nodes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DialogueScripts : MonoBehaviour
{
    public DSGraph DSGraph;

    public TMP_Text text;

    public Transform buttonContainer; // Контейнер для кнопок

    public Button prefabButton;

    public int numberOfButtons = 5; // Количество кнопок для генерации

    public DialogueNode CurrentNode; 

    private void Start()
    {
        var entryPoint = DSGraph.EntryPoint;
        var port = entryPoint.GetOutputPort(nameof(entryPoint.Start));
        CurrentNode = port.Connection.node as DialogueNode;
        Init();

    }

    private void GenerateButtons(DialogueNode dialogueNode)
    {
        // Удаляем все дочерние элементы из контейнера
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

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
            newButton.onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    private void OnButtonClick(int index)
    {
        Debug.Log("index");

        CurrentNode = CurrentNode.GetOutputPort(nameof(CurrentNode.Answers) + " " + index)?.Connection?.node as DialogueNode;

        if (CurrentNode == null) {
            UnityEngine.Application.Quit();
            return;
        }
        Init();
    }
    private void Init()
    {
        text.text = CurrentNode.Text;
        GenerateButtons(CurrentNode);
    }
}

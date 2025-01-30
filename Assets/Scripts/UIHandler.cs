using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }
    private VisualElement m_HealthBar;

    public float displayTime = 4f;
    private VisualElement m_NonPlayerDialogue;
    private Label helpRobotText;
    private float m_TimerDisplay;

    //Booleanos
    bool firstTime;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar= uiDocument.rootVisualElement.Q<VisualElement>("HealthBar1");
        SetHealthValue(1.0f);

        //Para el dialogo
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        helpRobotText = m_NonPlayerDialogue.Q<Label>("HelpRobotText");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        //Booleanos
        firstTime = true;
    }
    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay <= 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }
    public void SetHealthValue(float percentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * percentage);
    }
    public void DisplayDialogue()
    { 
        m_NonPlayerDialogue.style.display= DisplayStyle.Flex;
        if (firstTime == false)
        {
            helpRobotText.text = "Usa las flechas, WASD o el mando para moverte. Con el espacio o la X usas tu habilidad \r\n\r\nSi vas hacia el norte ten cuidado con las zonas de peleas. He escuchado que se reunen por esa zona";
        }
        else 
        {
            helpRobotText.text = "Hola! Ayúdame a arreglar los robots estropeados.";
        }
        firstTime = false;
        m_TimerDisplay = displayTime;
    }
    public void DisplayDialogueBattle()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        helpRobotText.text = "Esta zona esta hecha para los mejores. Si entras te enfrentaras a una horda de robots. Arreglalos a todos y álzate como el mejor";
        m_TimerDisplay = displayTime;
        EnemiesContainer.instance.SumaConfined();
    }
    public void DisplayDialogueRana()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        helpRobotText.text = "Que lindo día. En el sur hay aguas termales que te ayudan a recuperar la salud. \r\n\r\nSi en algún moento te sientes mal recuerda visitarlas";
        firstTime = false;
        m_TimerDisplay = displayTime;
    }
}

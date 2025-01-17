using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }
    private VisualElement m_HealthBar;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar= uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }
    public void SetHealthValue(float percentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * percentage);
    }
}

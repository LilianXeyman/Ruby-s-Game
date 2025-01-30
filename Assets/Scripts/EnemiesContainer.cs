using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesContainer : MonoBehaviour
{
    public static EnemiesContainer instance;

    int enemysFixed;
    int enemys;
    int enemyConfined;

    AudioSource audioSource;
    public AudioClip questCompleted;

    [SerializeField]
    TextMeshProUGUI textEnemysLeft;

    [SerializeField]
    GameObject enemyParent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        //AddEnemie();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }
    public void RemoveEnemie()
    {
        enemysFixed += 1;
        ActualizarEtiqueta();
        if(enemysFixed==enemys)
        {
            PlayerController.instance.PlaySound(questCompleted);
            Debug.Log("Ha sonado la musica");
        }
    }
    public void AddEnemie()
    {
        //enemys = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemys +=1;
        ActualizarEtiqueta ();
    }
    public void ActualizarEtiqueta()
    {
        textEnemysLeft.text = enemysFixed.ToString() + "/" + enemys.ToString();
    }
    public void SumaConfined()
    {
        enemyConfined = enemyParent.transform.childCount;
        enemys += enemyConfined;
        ActualizarEtiqueta();
    }
}

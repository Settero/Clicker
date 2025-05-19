using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;
using System.Collections;
public class Main : MonoBehaviour
{

    public long money, knowledge;
    public TextMeshProUGUI MoneyText, KnowledgeText;
    public float kadd = 1f, kmultiplier = 1f, knowledgePassive=0f, MoneyPassive=1f;

    private void Buster_analis()
    {
        Busters buster1 = new Busters();
        buster1.buster_name = "buster1";
        buster1.buster_bonus = 1f;
        buster1.buster_kol = 2;
    }


    private void Start()
    {
        StartCoroutine(PassiveEarn());
    }
    
    
    private IEnumerator PassiveEarn()
    {
        while (true)
        {
            knowledge += Convert.ToInt64(knowledgePassive);
            money += Convert.ToInt64(MoneyPassive);
            yield return new WaitForSeconds(1f);
        }
    }


    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            knowledge += Convert.ToInt64(kadd * kmultiplier);
            }

        KnowledgeText.text = knowledge.ToString();
        MoneyText.text = money.ToString();
    }
}

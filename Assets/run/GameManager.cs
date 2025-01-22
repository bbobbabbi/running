using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public float speed = 10;
    public PlayerInfo player;
    public Canvas ui;
    public GameObject panel;
    private TextMeshProUGUI tx;
    private TextMeshProUGUI panelTx;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(count());
        tx = ui.GetComponentInChildren<TextMeshProUGUI>();

        foreach (Transform child in ui.transform)
        {
            if (child.GetComponent<UnityEngine.UI.Image>() != null)
            {
                panel =child.gameObject;
                panel.SetActive(true);
            }
        }
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tx.text = "GAS : " + player.gas;
        if (player.gas <= -10) {
            GameOver();
        }
    }

    private void GameOver()
    {
        panel.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator count() {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            player.gas -=10;
        }
    }

    public void GameResetButtonEvent() {
        Time.timeScale = 1;
        int pizzaLayer = LayerMask.NameToLayer("pizza");

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == pizzaLayer)
            {
                Destroy(obj);
                Debug.Log("PizzaLayer에 속한 오브젝트가 삭제되었습니다.");
            }
        }
        player.gas = 100;
        player.transform.position = player.firstPosition;
        player.transform.rotation = Quaternion.identity;
        panel.SetActive(false);
    }
}

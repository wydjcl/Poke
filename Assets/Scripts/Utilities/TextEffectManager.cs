using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class TextEffectManager : MonoBehaviour
{
    public static TextEffectManager Instance;
    public GameObject TextPrefab;
    public TextMeshPro Text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TextUpEffect(string t, GameObject trans)
    {
        GameObject TextP = Instantiate(TextPrefab, trans.transform.position, Quaternion.identity, trans.gameObject.transform);
        TextMeshPro tT = TextP.GetComponentInChildren<TextMeshPro>();
        tT.text = t;
        Debug.Log("chufa");
        TextP.transform.DOMove(new Vector3(trans.transform.position.x, trans.transform.position.y + 3f, 0), 0.7f).SetEase(Ease.OutQuad).onComplete = () =>
        {
            tT.DOColor(new Color(0, 0, 0), 1f).onComplete = () =>
            {
                TextP.transform.DOKill();
                tT.DOKill();
                Destroy(TextP);
            };
        };
    }
}
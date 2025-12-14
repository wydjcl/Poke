using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInBag : MonoBehaviour, IPointerClickHandler
{
    public StringEventSO nameEvent;
    public StringEventSO desEvent;
    public string nameText;
    public string desText;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        nameEvent.RaisEvent(nameText, this);
        desEvent.RaisEvent(desText, this);
    }
}
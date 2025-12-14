using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PetMana : MonoBehaviour
{
    public GameObject petManaPrefab;
    public int petMaxMana;
    public PetDataSO petData;
    public float maxLenght = 1f;
    public List<GameObject> manaPrefabList;

    // Start is called before the first frame update
    private void Start()
    {
        if (transform.parent.parent.GetComponent<CharacterBase>().isEnemy)
        {
            petData = transform.parent.parent.GetComponent<CharacterBase>().enemyData;
        }
        else
        {
            petData = transform.parent.parent.GetComponent<CharacterBase>().petData;
        }

        petMaxMana = petData.maxMana;

        for (int i = 0; i < petMaxMana; i++)
        {
            if (petMaxMana % 2 == 0)
            {
                var m = Instantiate(petManaPrefab, this.transform);
                m.transform.localPosition = new Vector3((i - ((petMaxMana - 1f) / 2f)) * (maxLenght / (petMaxMana)), 0f, 0f);

                manaPrefabList.Add(m);
            }
            else
            {
                if (petMaxMana == 1)
                {
                    var m = Instantiate(petManaPrefab, transform);
                    manaPrefabList.Add(m);
                }
                else
                {
                    var m = Instantiate(petManaPrefab, transform);
                    m.transform.localPosition = new Vector3((i - petMaxMana / 2) * (maxLenght / (petMaxMana - 1)), 0f, 0f);
                    manaPrefabList.Add(m);
                }
            }
        }
        ChangeManaUI(0);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //ÐÞ¸ÄË®¾§
    public void ChangeManaUI(int count)
    {
        /*  for (int i = 0; i < count; i++)
          {
          }*/
        for (int i = 0; i < manaPrefabList.Count; i++)
        {
            if (i < count)
            {
                SpriteRenderer s = manaPrefabList[i].transform.Find("ManaSprite").GetComponent<SpriteRenderer>();
                // SpriteRenderer s = manaPrefabList[i].GetComponent<SpriteRenderer>();

                s.color = new Color(1f, 1f, 1f);
            }
            else
            {
                //SpriteRenderer s = manaPrefabList[i].GetComponent<SpriteRenderer>();
                SpriteRenderer s = manaPrefabList[i].transform.Find("ManaSprite").GetComponent<SpriteRenderer>();

                s.color = new Color(0.4f, 0.4f, 0.4f);
            }
        }
    }

    public void StartManaUI()
    {
    }
}
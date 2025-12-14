using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int ID;
    public PlayerType playerType;
    public string cardName;
    public int newHealth;
    public int newMana;
    public int newAttack;
    public Sprite playerImage;
}
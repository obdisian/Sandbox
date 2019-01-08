using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "GameParam", menuName = "Param/GameParam")]
public class GameParam : ScriptableObject
{
}

[CreateAssetMenu(fileName = "CharacterParam", menuName = "Param/CharacterParam")]
public class CharacterParam : ScriptableObject
{
    public GameObject PlayerPrefab = null;
    public GameObject EnemyPrefab = null;
    public GameObject UICanvas = null;
    public List<GameObject> SkinPrefabs = new List<GameObject>();
    public int SkinIndex { get { return PlayerPrefs.GetInt("SkinIndex", 0); } set { PlayerPrefs.SetInt("SkinIndex", value); } }
    public List<GameObject> EffectPrefabs = new List<GameObject>();
}

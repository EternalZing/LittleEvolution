using System;
 
using UnityEngine;
/// <summary>
///
/// copy from wiki
/// 
/// </summary>
/// <see cref="http://wiki.unity3d.com/index.php/SerializableDictionary"/>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> {}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Malee;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    [Reorderable(paginate = false)]
    public ArrayofAchievements achievementlist;

    [System.Serializable]
    public class ArrayofAchievements : ReorderableArray<Achievements> { }
}

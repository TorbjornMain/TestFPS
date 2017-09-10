using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(Damageable))]
public class DamageableEditor : Editor {
    bool showResistances = true;
    public override void OnInspectorGUI()
    {
        Damageable curDam = (Damageable)target;
        showResistances = EditorGUILayout.Foldout(showResistances, "Susceptabilites");
        
        if(curDam.susceptability == null)
        {
            curDam.susceptability = new Dictionary<DamageType, float>();
            for(int i = 0; i <= (int)DamageType.True; i++)
            {
                curDam.susceptability.Add((DamageType)i, 1);
            }
        }

        if (showResistances)
        {
            for (int i = 0; i < (int)DamageType.True; i++)
            {
                curDam.susceptability[(DamageType)i] = EditorGUILayout.FloatField(((DamageType)i).ToString() + " Susceptability", curDam.susceptability[(DamageType)i]);
            }
            EditorGUILayout.LabelField("");
        }
        curDam.MaxHealth = EditorGUILayout.DelayedFloatField("Max Health", curDam.MaxHealth);
        curDam.Health = EditorGUILayout.DelayedFloatField("Current Health", curDam.Health);

        DrawDefaultInspector();
    }
}
#endif
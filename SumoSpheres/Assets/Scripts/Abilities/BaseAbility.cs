using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    public string m_AbilityName;
    public KeyCode m_AbilityMapping;
    public float m_CoolDown;
    public AudioClip m_AbilitySound;

    public abstract void Initialize();
    public abstract void TriggerAbility();
}

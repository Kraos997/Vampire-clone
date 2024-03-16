using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BaseStatue : MonoBehaviour
{
    [SerializeField] private Transform centrumPivot;

    public virtual void Interact(PlayerScript player)
    {
        Debug.LogError("BaseStatue.Interact();");
    }
    public virtual void InteractAlternate(PlayerScript player)
    {
        Debug.LogError("BaseStatue.InteractAlternate();");
    }
}

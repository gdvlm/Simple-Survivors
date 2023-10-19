using System;
using UnityEngine;

namespace SimpleSurvivors.Variables
{
    [CreateAssetMenu(fileName = "Int Template", menuName = "ScriptableObjects/Variables/Int")]
    public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        public int InitialValue;

        [NonSerialized] public int RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
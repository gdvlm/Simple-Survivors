using System;
using UnityEngine;

namespace SimpleSurvivors.Variables
{
    [CreateAssetMenu(fileName = "Float Template", menuName = "ScriptableObjects/Variables/Float")]
    public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        public float InitialValue;

        [NonSerialized]
        public float RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize() { }
    }
}

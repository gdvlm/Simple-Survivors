using System;
using SimpleSurvivors.Player;
using UnityEngine;

namespace SimpleSurvivors.Variables
{
    [CreateAssetMenu(fileName = "Player Direction Template", menuName = "ScriptableObjects/Variables/Player Direction")]
    public class PlayerDirectionVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        public PlayerDirection InitialValue;

        [NonSerialized]
        public PlayerDirection RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize() { }
    }
}

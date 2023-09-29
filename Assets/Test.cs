using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSurvivors
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}

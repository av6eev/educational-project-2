using System.Collections;
using UnityEngine;

namespace InputManager
{
    public class InputComponent : MonoBehaviour
    {
        public void OnDisableAction(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    }
}
using UnityEngine;

namespace Slimecing.SOEventSystem
{
    public class TestEvents : MonoBehaviour
    {
        public void TestThisEvent(object thing)
        {
            Debug.Log("Here is the " + thing);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class WeakSpotButton : MonoBehaviour
{
    //Weakspot Button Command
    public void TaskOnClick()
    {
        GameObject.Find("Enemy").GetComponent<WeakSpot>().CriticalHit();
    }
}

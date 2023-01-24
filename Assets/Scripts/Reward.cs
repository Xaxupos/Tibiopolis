using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public Button button;

    public void SetReward(UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
}
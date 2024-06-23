using System.Collections;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    //UI elements
    [SerializeField] private GameObject notification;

    private void Start()
    {
        notification.SetActive(false);
    }

    //Turns on the notification for the specified duration
    public void ActivateForTime(float duration)
    {
        StartCoroutine(ActivateCoroutine(duration));
    }

    private IEnumerator ActivateCoroutine(float duration)
    {
        notification.SetActive(true);
        yield return new WaitForSeconds(duration);
        notification.SetActive(false);
    }
}

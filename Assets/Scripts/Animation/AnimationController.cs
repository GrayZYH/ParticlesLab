using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator ShootingStarAnim;
    public Animator StarAnim;
    public float StartShootingAnimTimes;
    public float StartStarAnimTime;

    private void Start()
    {
        StartCoroutine(StartShootingAnim());
        StartCoroutine(StartStarAnim());
    }

    private IEnumerator StartShootingAnim()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(StartShootingAnimTimes);
            ShootingStarAnim.gameObject.SetActive(true);
            ShootingStarAnim.enabled = true;
            ShootingStarAnim.Play("ShootingStartAnim", 0, 0.0f);
            ShootingStarAnim.Update(0.0f);
        }
    }

    private IEnumerator StartStarAnim()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(StartStarAnimTime);
            StarAnim.gameObject.SetActive(true);
            StarAnim.enabled = true;
            StarAnim.Play("StartAnim", 0, 0.0f);
            StarAnim.Update(0.0f);
        }
    }
}

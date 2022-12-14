using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rake : MonoBehaviour
{
    [SerializeField] private Animator rakeAnimator;
    [SerializeField] private AudioSource audio;
    private PlayerManager playerManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            rakeAnimator.SetTrigger("Bump");
            StartCoroutine("AnimationWait");
            playerManager = other.GetComponent<PlayerManager>();
        }

    }
    IEnumerator AnimationWait()
    {
        audio.Play();
        yield return new WaitForSeconds(0.4f);
        playerManager.DealDamage(100);
        rakeAnimator.ResetTrigger("Bump");
    }
}
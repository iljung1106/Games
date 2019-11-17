using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator animator;
    GameManager gameManager;
    public float dyingTime = 0;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<CharacterController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BossDie()
    {
        animator.Play("Die");
        Debug.Log("DieStart");
        yield return new WaitForSeconds(dyingTime);
        Debug.Log("DieEnd");
        Destroy(player);
        gameManager.LoadMenu();
    }

    public void Die()
    {
        BossDie();
        StartCoroutine("BossDie");
    }
}

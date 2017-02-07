using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{

    public bool isStartingRoom;
    public bool isActiveRoom;
    public bool isExitRoom;
    public bool isEnemyInside;
    public Color spriteColor = Color.white;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 3f;
    public float delayToFadeOut = 5f;
    public float delayToFadeIn = 5f;

    private SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (this.isStartingRoom || this.isActiveRoom)
        {
            StartCoroutine("FadeCycle");
        }

    }
    void OnMouseDown()
    {
        StartCoroutine("FadeCycle");
    }

    //void OnCollisionStay(Collision col)
    //{
    //    Debug.Log("entra");
    //    if (col.gameObject.tag == "Player")
    //    {
    //        Debug.Log("va");
    //    }
    //}

    void OnTriggerEnter2D(Collider2D cols)
    {
        Debug.Log("entra");
        if (cols.gameObject.CompareTag("Player"))

            Debug.Log("va");
    }

    IEnumerator FadeCycle()
    {
        float fade = 0f;
        float startTime;
        while (true)
        {
            startTime = Time.time;
            while (fade < 1f)
            {
                fade = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInTime);
                spriteColor.a = fade;
                sprite.color = spriteColor;
                yield return null;
                //print("Step 1");
            }


            fade = 1f;
            spriteColor.a = fade;
            sprite.color = spriteColor;
            yield return new WaitForSeconds(delayToFadeOut);
            //print("Step 2");

            startTime = Time.time;
            while (fade > 0f)
            {
                fade = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeOutTime);
                spriteColor.a = fade;
                sprite.color = spriteColor;
                yield return null;
                //print("Step 3");
            }
            fade = 0f;
            spriteColor.a = fade;
            sprite.color = spriteColor;
            yield return new WaitForSeconds(delayToFadeIn);
            sprite.enabled = false;
            StopCoroutine("FadeCycle");
        }
    }
}


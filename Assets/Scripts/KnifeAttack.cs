using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private Knife knife;
    private Animator anim;
 
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        DoAttackOnInput(Input.GetKeyDown(KeyCode.Mouse0));
    }


     void DoAttackOnInput(bool keycodePresseDown)
    {
        if (keycodePresseDown)
        {
            anim.SetTrigger("AttackBtnPressed");
            StartCoroutine(Attack(0.1666667f));
        }
    }

    AnimationClip GetAnimationClip(string name)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }


    IEnumerator Attack(float delay)
    {
        float y_position = 1.271f;
        float animClipLength = GetAnimationClip("knife_meleeattack").length;
        float y_bound_size = 1.457f;
        float secondsPerStep = (0.4833f - 0.1666667f) / 40f;
        float stepAmount = y_bound_size/40f + 0.093f;
        float elapsedTime = 0;
        Transform hitbox = gameObject.GetComponent<Transform>().GetChild(0);

        yield return new WaitForSeconds(delay);

        SetHitBox(true);

        while (y_position > -1.083f)
        {
            if (elapsedTime >= secondsPerStep)
            {
                elapsedTime = 0;
                y_position -= stepAmount;
                float x_position = KnifePositionOverTime(y_position);
                

                hitbox.localPosition = new Vector2(x_position, y_position);

                if (y_position <= -1.083f)
                {
                    y_position = -1.083f;
                }
            }

            if (hitbox.GetComponent<AnimFrameHitbox>().Hit)
            {
                Debug.Log("hit!");
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hitbox.localPosition = Vector2.zero;
        SetHitBox(false);

    }

    float KnifePositionOverTime(float y)
    {
        if (y < -1.083f)
        {
            return 0.683f;
        }
        else if (y < -0.185f)
        {
            return 1.29f - Mathf.Pow(10, -y - 1.3f);
        }
        else if (y <= 1.271f)
        {
            return 1.25f - Mathf.Pow(10, y - 1.25f);
        }
        else if (y > 1.271f)
        {
            return 0.2f;
        }
        else
        {
            return -1;
        }
    }

    void SetHitBox(bool status)
    {
        transform.GetChild(0).gameObject.SetActive(status);
    }
}

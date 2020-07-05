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

    }

    public void Attack()
    {
        Debug.Log("Knife Attack");
        anim.SetTrigger("AttackBtnPressed");
        StartCoroutine(Atk());
    }


    IEnumerator Atk()
    {
        float animClipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        int iterations = 30;
        int i = 0;
        float step = animClipLength / 30;

        while (i < iterations)
        {
            float new_x_position = KnifePositionOverTime(step);
            float new_y_position = step;
            Transform hitbox = gameObject.GetComponent<Transform>().GetChild(0);
            hitbox.position = new Vector2(new_x_position, new_y_position);

            yield return null;
        }

    }

    float KnifePositionOverTime(float y)
    {
        if (y < -1.083f)
        {
            return 0.683f;
        }
        else if (y < -0.185f)
        {
            return 1.29f - Mathf.Pow(10, -y - 1.9f);
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
}

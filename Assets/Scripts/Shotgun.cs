using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Firearm
{

    override protected void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearm.CurrentMagazineCapacity == 0);

        if (keycodePressedDown && !needToReload && !firearm.AttackInTimeout && !reloading)
        {
            Vector3 aimControllerPosition = aimController.transform.position;
            Vector2[] bulletTravelDirections = new Vector2[3];
            Transform[] bulletClones = new Transform[3];
            Rigidbody2D[] bulletCloneRBs = new Rigidbody2D[3];


            bulletTravelDirections[0] = RotateVector(aimController.LookDirection, 3);
            bulletTravelDirections[1] = aimController.LookDirection;
            bulletTravelDirections[2] = RotateVector(aimController.LookDirection, -3);

            bulletClones[0] = Instantiate(bullet);
            bulletClones[1] = Instantiate(bullet);
            bulletClones[2] = Instantiate(bullet);

            bulletCloneRBs[0] = bulletClones[0].GetComponent<Rigidbody2D>();
            bulletCloneRBs[1] = bulletClones[1].GetComponent<Rigidbody2D>();
            bulletCloneRBs[2] = bulletClones[2].GetComponent<Rigidbody2D>();



            anim.SetTrigger("Shoot");
            aud.PlayClip(attackAudioClipName);
            muzzle_flash.gameObject.SetActive(true);
            firearm.AddToCurrentMagCapacity(-1);
            firearm.AttackTimeoutClock.ResetClock();

            for (int i = 0; i < 3; i++)
            {
                bulletClones[i].gameObject.SetActive(true);
                bulletClones[i].GetComponent<DamageSource>().SetDamageValue(firearm.DamageValue);
                bulletClones[i].position = aimControllerPosition;
                bulletCloneRBs[i].velocity = bulletTravelDirections[i] * speed;
            }

        }
        else
        {
            muzzle_flash.gameObject.SetActive(false);
        }
    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {
        Vector2 rotated;
        degrees *= Mathf.Deg2Rad;
        rotated.x = v.x * Mathf.Cos(degrees) - v.y * Mathf.Sin(degrees);
        rotated.y = v.x * Mathf.Sin(degrees) + v.y * Mathf.Cos(degrees);
        return rotated;
    }
}

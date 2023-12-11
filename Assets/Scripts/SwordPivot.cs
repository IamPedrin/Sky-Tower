using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPivot : MonoBehaviour
{

    public GameObject myPlayer; 
    public Animator animator; 
    public float delay = 0.3f;
    private bool attackBlock;

    private void FixedUpdate(){
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);


        //Flipa a espada para onde o mouse está apontando
        if(rotation_z < -90 || rotation_z > 90){
            if(myPlayer.transform.eulerAngles.y == 0){
                transform.localRotation = Quaternion.Euler(180f, 0f, -rotation_z);
            }
        }
    }

    public void Attack(){
        animator.SetTrigger("swordAttack");
    }

}

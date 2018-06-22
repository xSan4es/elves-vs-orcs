using UnityEngine;

public class Enemy : Hero
{
    private float way = 10;
    
    protected override void Move()
    {
        if (way > 0)
        {
            myTransform.position -= new Vector3((float)1 / 20, 0, 0);
            way -= (float)1 / 20;
        }
        else
            animator.SetTrigger("idle");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    protected bool isFacingRight = true;
    protected Transform myTransform;
    protected Animator animator;
    protected InfoHero myInfo;
    public int maxHealth;
    protected int health;
    public Scrollbar scrollBar;
    public Sprite[] skills = new Sprite[4];
    public string[] description = new string[4];
    public int[] skillDamage = new int[4]; // в подальшому це буде об'єкти класу Skill, де будуть прописані вже всякі фішечки (фіз. чи маг. урон/манакост т.д.)

    protected void Start()
    {
        myTransform = transform;
        animator = myTransform.GetComponent<Animator>();

        myInfo = new InfoHero();
        myInfo.img = skills;
        myInfo.description = description; //така інфа потім буде зчитуватись з бд
        myInfo.skillDamage = skillDamage;
        myInfo.script = gameObject.GetComponent<Hero>();

        health = maxHealth;
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected virtual void OnMouseDown()
    {
        if (GameController.isActivatedSkill && GameController.isBattle)
            GameController.AnimatedAttack(myInfo);
        else
            GameController.TakeHeroInfo(myInfo);
    }

    protected virtual void Move()
    {
        float move = Parallax.move;

        if(move == 0)
            animator.SetTrigger("idle");
        else
            animator.SetTrigger("walk");

        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = myTransform.localScale;
        theScale.x *= -1;
        myTransform.localScale = theScale;
    }

    public void TakeDamage(int damage) //коли скіл буде розписаний в класі, цей метод буде приймати його в якості вхідних даних і обробляти на іншому рівні
    {
        animator.SetTrigger("hurt");
        health -= damage;
        scrollBar.size = (float)health / maxHealth;
        if (health > maxHealth)
            health = maxHealth;
        if (health <= 0)
            animator.SetTrigger("die");
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    // кожен наслідник вже по своєму описує скіл і викликає потрібну анімацію і т.д.
    public virtual void FirstSkill()
    {
        animator.SetTrigger("attack");
    }

    public virtual void SecondSkill()
    {
        animator.SetTrigger("attack");
    }

    public virtual void ThirdSkill()
    {
        animator.SetTrigger("attack");
    }

    public virtual void FourthSkill()
    {
        animator.SetTrigger("attack");
    }
}

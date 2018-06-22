using UnityEngine;
using UnityEngine.UI;

// зараз інфа героя передається при кожному кліку по ньому
// якщо це лишнє, то можна тільки раз в старті на початку ігрового процесу зробити
public class GameController : MonoBehaviour
{
    public static bool isBattle;
    public static bool isActivatedSkill;

    public GameObject enemy;
    public Transform posBattle;
    public Animator[] animator;
    public static Image[] img;
    public static Text[] description;
    public static InfoHero hero;
    public static int tempSkill;

    private void Start()
    {
        isBattle = false;
        isActivatedSkill = false;

        img = new Image[4];
        description = new Text[4];

        for (int i = 0; i < 4; i++)
        {
            Transform temp = GameObject.FindGameObjectWithTag("Panel").transform.GetChild(0).GetChild(i);
            img[i] = temp.GetComponent<Image>();
            description[i] = temp.GetChild(0).GetChild(0).GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (posBattle.position.x < -50)
        {
            enemy.SetActive(true);
            isBattle = true;
            Parallax.move = 0;
        }
    }

    public void addPanel(bool check)
    {
        for (int i = 0; i < animator.Length; i++)
            animator[i].SetBool("isUp", check);
    }

    public static void TakeHeroInfo(InfoHero hero)
    {
        for (int i = 0; i < 4; i++)
        {
            img[i].sprite = hero.img[i];
            description[i].text = hero.description[i];
        }
        GameController.hero = hero;
    }

    public static void AnimatedAttack(InfoHero target)
    {
        target.script.TakeDamage(hero.skillDamage[tempSkill]);

        switch (tempSkill)
        {
            case 0:
                {
                    hero.script.FirstSkill();
                    break;
                }
            case 1:
                {
                    hero.script.SecondSkill();
                    break;
                }
            case 2:
                {
                    hero.script.ThirdSkill();
                    break;
                }
            case 3:
                {
                    hero.script.FourthSkill();
                    break;
                }
        }

        isActivatedSkill = false;
        Time.timeScale = 1f;
    }

    public void ActivatedSkill(int number)
    {
        if (isBattle)
        {
            isActivatedSkill = true;
            Time.timeScale = 0.3f;
            tempSkill = number;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

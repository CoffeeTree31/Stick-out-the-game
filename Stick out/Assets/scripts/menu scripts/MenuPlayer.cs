using UnityEngine;
using UnityEngine.UI;

public class MenuPlayer : MonoBehaviour
{
    public GameObject goal1;
    public GameObject goal2;
    public float speed = 3f;
    private float rand;
    bool check = false;
    public GameObject trail;


    public Text maxscore_text;
    public int maxscore = 0;

    private void Start()
    {
        rand = Random.Range(0, 3);
        goal2.transform.position = new Vector2(Random.Range(-1.6f, 1.6f), Random.Range(-1.2f, 1.8f));
        goal1.transform.position = new Vector2(Random.Range(-1.6f, 1.6f), Random.Range(-1.2f, 1.8f));
        if (rand > 1)
        {
            goal1.SetActive(false);
        }
        else
        {
            check = !check;
            goal2.SetActive(false);
        }
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            maxscore = PlayerPrefs.GetInt("MaxScore");
        }
        maxscore_text.text = maxscore.ToString();
    }
    private void Update()
    {
        if (check) transform.position = Vector3.MoveTowards(transform.position, goal1.transform.position, speed * Time.deltaTime);
        if (!check) transform.position = Vector3.MoveTowards(transform.position, goal2.transform.position, speed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0f, 0f, 0.7f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "goal1")
        {
            check = !check;
            goal1.SetActive(false);
            goal2.transform.position = new Vector2(Random.Range(-1.6f, 1.6f), Random.Range(-1.2f, 1.8f));
            goal2.SetActive(true);
        }
        if (collision.tag == "goal2")
        {
            check = !check;
            goal2.SetActive(false);
            goal1.transform.position = new Vector2(Random.Range(-1.6f, 1.6f), Random.Range(-1.2f, 1.8f));
            goal1.SetActive(true);
        }
    }
    public void Checker()
    {
        check = !check;
    }
}

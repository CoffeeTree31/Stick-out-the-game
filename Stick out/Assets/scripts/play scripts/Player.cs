using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
    //в этом коде нет комментариев, потому что я ленивая жопа 
    public GameObject goal1;
    public GameObject goal2;
    public float speed = 3f;
    public int score;
    public Text score_text;
    private float rand;
    bool check = false;
    public GameObject RestartButton;
    private bool timer_start = false;
    public float timer = 1f;
    public GameObject trail;
    public GameObject death_particle;
    public GameObject BGmusic;
    public Text maxscore_text;
    public int maxscore = 0;

    public GameObject tap_to_move_text;

    private void Start()
    {
        //РЕКЛАМА
        if (Advertisement.isSupported)
            Advertisement.Initialize("4519507");

        rand = Random.Range(0, 3);
        goal2.transform.position = new Vector2(Random.Range(-1.44f, 1.44f), -1.9f);
        goal1.transform.position = new Vector2(Random.Range(-1.44f, 1.44f), 2.5f);
        if (rand > 1)
        {
            goal1.SetActive(false);
            transform.position = goal2.transform.position;
            check = !check;
        }
        else
        {
            goal2.SetActive(false);
            transform.position = goal1.transform.position;
        }
        score = -1;
    }
    private void Update()
    {
        if (check) transform.position = Vector3.MoveTowards(transform.position, goal1.transform.position, speed * Time.deltaTime);
        if (!check) transform.position = Vector3.MoveTowards(transform.position, goal2.transform.position, speed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0f, 0f, 0.7f);
        if (timer_start) { 
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                RestartButton.SetActive(true);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!timer_start)
        {
            if (collision.tag == "goal1")
            {
                score++;
                check = !check;
                goal1.SetActive(false);
                goal2.transform.position = new Vector2(Random.Range(-1.44f, 1.44f), -1.94f);
                goal2.SetActive(true);
                if (score > 10 && score < 20)
                {
                    speed = 2.8f;
                }
                else if (score > 20 && score < 35)
                {
                    speed = 2.6f;
                }
                else if (score > 35)
                {
                    speed = 2.5f;
                }
                score_text.text = score.ToString();
            }
            else if (collision.tag == "goal2")
            {
                score++;
                check = !check;
                goal2.SetActive(false);
                goal1.transform.position = new Vector2(Random.Range(-1.44f, 1.44f), 2.54f);
                goal1.SetActive(true);
                score_text.text = score.ToString();
            }
            else if (collision.tag == "board")
            {
                check = !check;

            }
            else if (collision.tag == "Stick")
            {
                trail.SetActive(false);
                death_particle.transform.position = gameObject.transform.position;
                death_particle.SetActive(true);
                gameObject.GetComponent<Renderer>().enabled = false;

                PlayerPrefs.SetInt("AdScore", PlayerPrefs.GetInt("AdScore") +score);
                //Реклама
                if (PlayerPrefs.GetInt("AdScore") > 150)
                {
                    BGmusic.SetActive(false);
                    Advertisement.Show("Interstitial_Android");
                    PlayerPrefs.SetInt("AdScore", 0);
                }

                timer_start = true;
                if (PlayerPrefs.GetInt("MaxScore") < score)
                {
                    PlayerPrefs.SetInt("MaxScore", score);
                }


                if (PlayerPrefs.HasKey("MaxScore"))
                {
                    maxscore = PlayerPrefs.GetInt("MaxScore");
                }
                tap_to_move_text.SetActive(false);
                maxscore_text.text += "MAX SCORE: " + maxscore.ToString();

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "board")
        {
            check = !check;

        }
    }
    public void Checker()
    {
        check = !check;
    }
}

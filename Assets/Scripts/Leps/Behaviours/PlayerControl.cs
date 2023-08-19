using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerControl : MonoBehaviour
{
    //Toc do di chuyen
    [SerializeField] float speed;
    [SerializeField] bool isRight;
    [SerializeField] bool isOnGround;

    //Animetion
    private Animator animator;
    private float animatorSpeed;
    private bool animatorIsOnGround;

    //đồng xu 
    [SerializeField] TextMeshProUGUI coinText;
    private int coins;

    //quả thông 
    [SerializeField] TextMeshProUGUI thongText;
    private int quaThongs;

    //điểm diệt quái 
    [SerializeField] TextMeshProUGUI diemQuaiText;
    private int diemQuais;

    //Gameover canvas 
    [SerializeField] Canvas canvasGameOver;
    

    //thời gian 
    private bool isAlive;
    private int  times;
    [SerializeField] TextMeshProUGUI timeGame;

    // Vien dan
    [SerializeField] GameObject item;

    //Am thanh
    public AudioSource soundMain, soundDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorSpeed = 0.0f;
        animatorIsOnGround = true;

        //So luong an dong xu
        coins = 0;
        coinText.text = coins + "";

        //Số lượng ăn quả thông 
        quaThongs = 0;
        thongText.text = quaThongs + "";

        //Số lượng diệt quái 
        diemQuais = 0;
        diemQuaiText.text = diemQuais + "";

        //An man hinh gameover
        canvasGameOver.gameObject.SetActive(false);

        //Thoi gian cua tro choi
        isAlive= true;
        times = 200;
        timeGame.text = times + "";
        StartCoroutine(UpdateTime());

        //Am thanh cua tro choi
        soundMain.Play();
        soundDead.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOnGround", animatorIsOnGround);
        animator.SetFloat("Speed", animatorSpeed);
        //Khi nhân vật sang phải 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            //
            //GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;

            isRight = true;
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;

            animatorSpeed = 1.0f;
        }
        //Khi nhân vật sang trái 
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //
            //GetComponent<Rigidbody2D>().velocity = Vector3.left * speed;

            isRight = false;
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;

            animatorSpeed = 1.0f;
        }
        //Khi nhân vật nhảy
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * speed;
            animatorIsOnGround = false;
        }
        else
        {
            animatorSpeed = 0.0f;
        }

        //bắn quả thông 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            quaThongs--;
            thongText.text = quaThongs + "";
            if(quaThongs >= 0)
            {
                GameObject _item = Instantiate<GameObject>(item);
                Vector3 playerPosition = transform.position;
                //tốc độ bay cua quả thông 
                _item.GetComponent<Item>().SetSpeed(10);
                //vị trí bắn của quả thông 
                _item.GetComponent<Item>().SetPosition(new Vector3(playerPosition.x, playerPosition.y));
                //Hướng bắn của quả thông 
                if (isRight)
                {
                    //bắn bên phải 
                    _item.GetComponent<Item>().SetDirection(true);
                }
                else
                {
                    //bắn bên trái 
                    _item.GetComponent<Item>().SetDirection(false);
                }
            }
            else
            {
                quaThongs = 0;
                thongText.text = quaThongs + "";
            }
           
            
        }
    }

    //50 fps
    private void FixedUpdate()
    {
        
    }
    // 2 collider va cham nhau ( true )
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // cộng điểm ăn đồng xu
        if (collision.gameObject.CompareTag("Coin")){
            coins ++;
            coinText.text = coins + "";
        }
        // cộng điểm ăn quả thông
        else if(collision.gameObject.CompareTag("quathong")){
            quaThongs++;
            thongText.text = quaThongs + "";
        }
    }

    // 2 collider va cham nhau ( false )
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Brick"))  
        {
            animatorIsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("npc"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x; // directionX == 1: phải, else: trái
            float directionY = direction.y; // directionY == 1: trên, else: dưới
            if(directionX > 0 || directionX > 0 || directionY < 0) 
            {
                //Nhân vật chết 
                Time.timeScale = 0;
                canvasGameOver.gameObject.SetActive(true);
                soundMain.Stop();
                soundDead.Play();
            }
            else
            {
                //Tiêu diệt quái 
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "NextMap")
        {
            SceneManager.LoadScene("Map2");
        }
        else if (collision.gameObject.tag == "VucTham")
        {
            Time.timeScale = 0;
            canvasGameOver.gameObject.SetActive(true);
            soundMain.Stop();
            soundDead.Play();
        }
    }

    //Thoát trò chơi 
    public void QuitGame()
    {
        Application.Quit();
    }

    //Restart lại màn mình vừa chơi 
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Đếm thời gian khi game bắt đầu 
    IEnumerator UpdateTime()
    {
        while(isAlive)
        {
            times--;
            timeGame.text = times + "";
            yield return new WaitForSeconds(1);
        }
    }
}

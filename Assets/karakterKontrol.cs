using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//ana menü sahne import

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] beklenenAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Text canText;
    public Image siyahArkaPlan;
    

    SpriteRenderer spriteRendere;
    
    Rigidbody2D fizik;
    Vector3 vec;

    bool birKereZipla=true;
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int can = 100;
    

    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float siyahArkaPlanSayacı = 0;
    float anaMenuyeDonZaman = 0;

    Vector3 vecc;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    GameObject kamera;

    


    


    


    void Start()
    {
        spriteRendere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);
        }

        

        kameraIlkPos = kamera.transform.position - transform.position;
        canText.text = "CAN" + can;

    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZipla = false;
            }
            
        }
    }

       
    void FixedUpdate()
    {
        karakterHaraket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            siyahArkaPlanSayacı += 0.03f;
            siyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayacı);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman>1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }
    }

     void LateUpdate()
    {
        kameraKontrol();
    }

    void karakterHaraket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y,0);
        fizik.velocity = vec;
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        birKereZipla = true;  
    }

     void OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.tag == "kursun")
        {
            can -= 5;
            canText.text = "CAN" + can;
        }
        if (col.gameObject.tag == "dusman")
        {
            can -= 10;
            canText.text = "CAN" + can;
        }
        if (col.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN" + can;
        }
    }

    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;//kamera son pozisyonu =kameranın ilk pozisyonu + karakterın hareketi sonucu yaptıgı pozisyon değiimi
        kamera.transform.position = Vector3.Lerp(kamera.transform.position,kameraSonPos,0.1f);//KAMERA YUMUSATMA İLK BASTA KENDİ POZİSYON SONRASINDA GİDİLİCEK OLAN KAMERA SON POS
    }


    void Animasyon()
    {

        if (birKereZipla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRendere.sprite = beklenenAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklenenAnim.Length)
                    {


                        beklemeAnimSayac = 0;

                    }
                    beklemeAnimZaman = 0;
                }


            }
            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {


                        yurumeAnimSayac = 0;

                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {


                        yurumeAnimSayac = 0;

                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (fizik.velocity.y > 0)
            {
                spriteRendere.sprite = ziplamaAnim[0];

            }
            else
            {
                spriteRendere.sprite = ziplamaAnim[1];
            }
            if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
            
    }
}

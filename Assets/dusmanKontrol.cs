using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
# endif

public class dusmanKontrol : MonoBehaviour
{
    public int resim;
    GameObject[] gidilecekNoktalar; //gidilecek objeleri hafızada tutmak için
    bool aradakiMesafeyiBirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayacı;
    bool ilerimiGerimi = true;
    GameObject karakter;
    RaycastHit2D ray;
    public LayerMask layermask;
    int hiz = 5;
    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriteRenderer;
    public GameObject kursun;
    float atesZamani = 0;



    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];//gidilecek noktalar alt birimler kadar
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        for (int i = 0; i < gidilecekNoktalar.Length; i++)

        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);

        }
    }


    void atesEt()
    {
        atesZamani += Time.deltaTime;
        if (atesZamani>Random.Range(0.2f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }


    void FixedUpdate()
    {
        beniGordumu();
        if (ray.collider.tag=="Player")
        {
            hiz = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();
        }
        else
        {
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;
        }



        
        noktalaraGit();
    }

    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position,rayYonum,1000,layermask);
        Debug.DrawLine(transform.position,ray.point,Color.magenta);
    }

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayacı].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayacı].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayacı == gidilecekNoktalar.Length - 1)
            {
                ilerimiGerimi = false;
            }
            else if (aradakiMesafeSayacı == 0)
            {
                ilerimiGerimi = true;
            }
            if (ilerimiGerimi)
            {
                aradakiMesafeSayacı++;
            }
            else
            {
                aradakiMesafeSayacı--;
            }
        }
    }

    public Vector2 getYon()//baska bi yerden buna ulasıp değiştirebiliyorum o değeri almak istiyorsam get programcılar arası kural gibi
    {
        return (karakter.transform.position - transform.position).normalized;
    }









#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;//daire renkleri grünmeye
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);//alt nesnelere daire cizdirme
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;//bunların arasındakı baglantılara lınelar rengı belırleme
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);//sphere arası line baglantısı kurma
        }
    }



#endif

}





#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]
class dusmanKontrolEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        dusmanKontrol script = (dusmanKontrol)target;
        if (GUILayout.Button("URET", GUILayout.MinWidth(100), GUILayout.Width(100)))// buton kuculttuk
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }

}


# endif

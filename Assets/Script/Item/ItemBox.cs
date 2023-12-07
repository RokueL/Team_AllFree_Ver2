using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public int ranHitCount;

    bool isHit;
    public int HitCount;
    public ObjectManager objectManager;
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        HitCount = 0;
        ranHitCount = 3;
    }

    IEnumerator OnHit()
    {
        if (isHit)
            yield break;

        isHit = true;
        sprite.color = new Color(1, 1, 1, 0.4f);
        //Item
        int ranItem= Random.Range(0,3);
        int ranCore = Random.Range(0, 3); 
        if (HitCount == ranHitCount)
        {
            gameObject.SetActive(false);
        }
        if (HitCount < ranHitCount)
        {
            for (int index = 0; index < 10; index++)
            {
                int ranPower = Random.Range(12, 18);
                Vector2 ranVec = new Vector2(Random.Range(-1f, 1f), 0);
                GameObject Item = objectManager.MakeObj("gold");
                Item.transform.position = transform.position;

                Rigidbody2D Item_Rigid = Item.GetComponent<Rigidbody2D>();
                Item_Rigid.AddForce(ranVec * 2 + Vector2.up * ranPower, ForceMode2D.Impulse);
            }
            HitCount++;
            yield return new WaitForSeconds(0.5f);
            sprite.color = new Color(1, 1, 0, 1);
            isHit = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어의 무기에 공격당했을 때 
        if (collision.gameObject.tag == "PlayerAttack")
        {
            StartCoroutine(OnHit());
        }
    }
}

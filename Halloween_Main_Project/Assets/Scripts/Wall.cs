    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite [] dmgSprite;
    public int hp = 5;
    public AudioClip chopSound1;
    public AudioClip chopSound2;


    private int count = 0;
    private SpriteRenderer spriteRenderer;
	
    // Use this for initialization
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int loss)
    {
        int index = 0;
        SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        if (count == 0)
        {
            index = 0;
            spriteRenderer.sprite = dmgSprite[index];
        }
        else if(count == 3)
        {
            index = 1;
            spriteRenderer.sprite = dmgSprite[index];
        }

        hp -= loss;
        if(count == 5)
        {
            //wall disapppears si su hp es menor a 0
            gameObject.SetActive(false);
        }
        

        count += 1;
    }
}

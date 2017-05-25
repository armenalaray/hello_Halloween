    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite [] dmgSprite;
    public int hp = 5;
    private int count = 0;
    private SpriteRenderer spriteRenderer;
	
    // Use this for initialization
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int loss)
    {
        if (count % 2 == 1 && count < 5)
        {
            int index = count / 2;
            spriteRenderer.sprite = dmgSprite[index];
        }

        hp -= loss;
        if(hp <= 0)
        {
            //wall disapppears si su hp es menor a 0
            gameObject.SetActive(false);
        }
        

        count += 1;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class World
{
    // We only can Instantiate Character Instance in the world
    // THINK: We can have multiple characters soon maybe???(think about it)
	// Yes, maybe AI allies?



    public Character character{ get; protected set;}

    public List<Character> enemies{ get; protected set; }

    Dictionary<string,Bullet> bulletPrototypes;
    
	public Dictionary<string,Weapon> weaponPrototypes;

	public World()
	{
        SetupWorld();
	}

    void SetupWorld()
    {
        bulletPrototypes = new Dictionary<string, Bullet>();
        weaponPrototypes = new Dictionary<string, Weapon>();

        
		CreatePrototypes();
		CreateCharacters();
		CreateEnemies();
    }

	void CreateCharacters()
	{
		// We only have one character, maybe later we'll have allias?
		// If we have, We need a character list.
		character = new Character();
		character.Type = "Main Character";

		// Default move speed in x-axis is +-5f.
		// Default jump speed in y-axis is 12f.
		character.speed = new Vector2(7f, 12f);

		// At the begining our characters looks right
		character.direction = Direction.Right;
		character.scale = new Vector3(1f, 1f, 0f);

	//	character.currentWeapon = weaponPrototypes["MP5"].Clone();
		
		Inventory ch_inventory = new Inventory();

        //bunun böyle olmaması lazım
		if(!PlayerPrefs.GetString("inventory").Equals(""))
		{
	        string[] myInv = PlayerPrefs.GetString("inventory").Split(',');

	        foreach (var item in myInv)
	        {
	        	if (weaponPrototypes.ContainsKey(item))
	            	ch_inventory.weapons[(int)weaponPrototypes[item].type] = weaponPrototypes[item].Clone();
	            else
	            	Debug.Log("item" + item + "yok");
	        }
	    }
       
        character.inventory = ch_inventory;

        //şuan hangi silah var bizde onu bilmiyoruz hangi silahı currWeapon yapacağız elimize varsayılan olarak bıçak mı vereceğiz napcaz????
        character.currentWeapon = character.inventory.weapons[0];
	}

	void CreateEnemies()
	{
		enemies = new List<Character>();

		for(int i = 0; i < 8; i++)
		{
			Character enemy = new Character();
			enemy.Type = "Enemy";

			// Default move speed in x-axis is +-3f.
			// Default jump speed in y-axis is 9f.
			enemy.speed = new Vector2(3f, 9f);

            enemy.direction = (Direction) Random.Range(1, 3);
			if(enemy.direction == Direction.Left)
				enemy.scale = new Vector3(-1f, 1f, 0f);
			else
				enemy.scale = new Vector3(1f, 1f, 0f);

			// FIXME: şimdilik oyunun akışı açısından silah atamasını rastgele yapıyorum
            if(Random.Range(0, 2) == 0)
				enemy.currentWeapon = weaponPrototypes["Magnum"].Clone();
            else
                enemy.currentWeapon = weaponPrototypes["MP5"].Clone();

			enemies.Add(enemy);
		}
	}

    //Bu çok uzun bir metod olacak parçalara ayıracağız muhtemelen her bir silah için ayrı bir metod yapmak daha okunaklı olacaktır
    void CreatePrototypes()
    {
  

        //Buraya bir mermi animasyon metodu yerleştirebiliriz yada ona benzer birşey....
        //THINK bunu burada mı yapmalıyız?
        //metodlar nerede olmalı

       // bulletPrototypes["Magnum"].RegisterBulletActionsCallback();

        CreateBulletProtos();

        CreateMagnumProto();
		
        CreateMP5Proto();

		CreateKnifeProto();

        CreateShotgunProto();

        CreateUziProto();
    }
   
}

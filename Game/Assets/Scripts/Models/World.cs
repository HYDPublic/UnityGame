﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
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
		character.speed = 5f;

		character.currentWeapon = weaponPrototypes["Magnum"].Clone();
	}

	void CreateEnemies()
	{
		enemies = new List<Character>();

		for(int i = 0; i < 9; i++)	// FIXME: Test amaçlı şuan 0 enemy üretiliyor.
		{
			Character enemy = new Character();
			enemy.Type = "Enemy";
			enemy.speed = 4f;
            enemy.direction = (Direction) Random.Range(1, 3);
			enemy.currentWeapon = weaponPrototypes["Magnum"].Clone();

			enemies.Add(enemy);
		}
	}

    //Bu çok uzun bir metod olacak parçalara ayıracağız muhtemelen her bir silah için ayrı bir metod yapmak daha okunaklı olacaktır
    void CreatePrototypes()
    {
        bulletPrototypes.Add(
            "Magnum",
            new Bullet("Magnum", 50f)
        );

        //Buraya bir mermi animasyon metodu yerleştirebiliriz yada ona benzer birşey....
        //THINK bunu burada mı yapmalıyız?
        //metodlar nerede olmalı

       // bulletPrototypes["Magnum"].RegisterBulletActionsCallback();

        weaponPrototypes.Add(
            "Knife",
            new Weapon("Knife")
        );

		weaponPrototypes["Knife"].weaponParameters.Add(
			"hitPower",
			5
		);

		weaponPrototypes["Knife"].weaponParameters.Add(
			"hitSpeed",
			2
		);



		weaponPrototypes.Add(
            "Magnum",
            new Weapon("Magnum", bulletPrototypes["Magnum"])
		);

        weaponPrototypes["Magnum"].cbAttack += WeaponActions.Magnum_One_Shot;


        weaponPrototypes["Magnum"].weaponParameters.Add(
			"fireFrequency",
			.1f	// .5 saniyede bir ateş edilebilir
		);

		weaponPrototypes["Magnum"].weaponParameters.Add(
			"fireCoolDown",
			-weaponPrototypes["Magnum"].weaponParameters["fireFrequency"]	// Başlangıç aniden ateş edebilmesi için gerekli
		);

        weaponPrototypes["Magnum"].weaponParameters.Add(
			"bulletCount",
			10
		);

        weaponPrototypes["Magnum"].weaponParameters.Add(
			"damage",
			0.2f
		);


    }


}
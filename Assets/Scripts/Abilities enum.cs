using UnityEngine;
using System.Collections;

public class Abilitiesenum : MonoBehaviour {

	enum Ability{Ability1, Ability2, Ability3}; void Start()
	{
		Ability player1_Abilities;
		Ability player2_Abilities;
		player1_Abilities = Ability.Ability1;
		player2_Abilities = Ability.Ability3;
	}

}

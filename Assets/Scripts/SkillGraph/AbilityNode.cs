using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class AbilityNode : SkillNode{
	[Input] public Func<GameObject> function;
	protected override void Init() {
		type = SKILLTYPE.ABILITY;
		
	}
	override public void OnEnter(){

	}
}

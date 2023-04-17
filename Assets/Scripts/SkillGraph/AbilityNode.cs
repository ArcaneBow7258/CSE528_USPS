using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[NodeTint("#00FF7F")]
public class AbilityNode : SkillNode{
	public ActiveAbility function;
	protected override void Init() {
		type = SKILLTYPE.ABILITY;
		
	}
	override public void OnEnter(){

	}
}

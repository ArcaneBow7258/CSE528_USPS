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
	public override void CreateData(SkillTree tree){
		base.CreateData(tree);
		talent.ability = function;
	}
	override public void OnEnter(){

	}
}

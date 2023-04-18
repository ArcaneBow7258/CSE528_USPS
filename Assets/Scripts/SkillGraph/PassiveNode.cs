using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[NodeTint("#efd7f6")]
public class PassiveNode : SkillNode{
	public List<passiveStat> stats;
	protected override void Init() {
		type = SKILLTYPE.PASSIVE;
		
	}
	public override void CreateData(SkillTree tree){
		base.CreateData(tree);
		talent.stat = stats;
	}
	override public void OnEnter(){

	}
}

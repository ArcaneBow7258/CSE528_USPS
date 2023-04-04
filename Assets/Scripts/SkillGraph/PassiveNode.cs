using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class PassiveNode : SkillNode{
	protected override void Init() {
		type = SKILLTYPE.PASSIVE;
		
	}
	
	override public void OnEnter(){

	}
}
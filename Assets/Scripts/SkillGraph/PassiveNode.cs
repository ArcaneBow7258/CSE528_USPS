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
	
	override public void OnEnter(){

	}
}
[Serializable]
public struct passiveStat{
	public STATTYPE tag;
	public float[] value;
}
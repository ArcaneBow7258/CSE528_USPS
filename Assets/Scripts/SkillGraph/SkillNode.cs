using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SkillNode : Node {
	[Input] public string id;
	[Input] public string skillName;
	[Input] public string skillDesc;
	
	[Input] public int dependencies;
	public enum NODETYPE{
		PASSIVE,
		ABILITY
	}
	[Output] public int children;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return GetInputValue<string>(port.fieldName); // Replace this
	}
}
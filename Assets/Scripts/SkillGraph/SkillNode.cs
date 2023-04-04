using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

abstract public class SkillNode : Node {
	public int id;
	public Sprite icon;
	public string skillName;
	public string skillDesc;
	
	private List<talent> dependencies;
	//[HideInInspector]
	public talent talent;
	[Input] public Empty  entry;
	[Output] public Empty exits;
	[HideInInspector]
	public SKILLTYPE type;
	
	

	// Use this for initialization
	protected override void Init() {
		base.Init();
		

		
		
	}
	abstract public void OnEnter();
	public void CreateData(){
		dependencies = new List<talent>();
		talent = new talent(id,this.position,icon,skillName,skillDesc, type, null);
		SkillTree.Instance.allTalents.Add(id,
            talent
            );
	}
	public void GetDependencies(){
		NodePort entryPort = GetInputPort("entry");
		if(!entryPort.IsConnected){
			return;
		}
		foreach(NodePort n in entryPort.GetConnections()){
			SkillNode correct = n.node as SkillNode;
			Debug.Log(correct.name);
			Debug.Log(correct.skillName);
			dependencies.Add(correct.talent);
		}
		talent.dependencies = dependencies;
	}
	public void Draw(){
		talent.draw();
	}
	/*
	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return GetInputValue<string>(port.fieldName); // Replace this
	}*/
	[Serializable]
	public class Empty{}
}


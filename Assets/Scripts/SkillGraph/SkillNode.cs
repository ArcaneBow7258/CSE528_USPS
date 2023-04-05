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
	
	private List<talent> dependencies= new List<talent>();
	private List<talent> children= new List<talent>();
	//[HideInInspector]
	public talent talent;
	[Input] public Empty  entry;
	[Output] public Empty exits;
	[HideInInspector]
	public SKILLTYPE type;
	
	

	// Use this for initialization
	protected override void Init() {
		base.Init();
		this.name = id + skillName;
		
	}
	abstract public void OnEnter();
	public void CreateData(SkillTree tree){
		
		talent = new talent(id,tree, this.position,icon,skillName,skillDesc, type);
		tree.allTalents.Add(id,
            talent
            );
	}
	public void GetLines(){
		NodePort entryPort = GetInputPort("entry");
		if(entryPort.IsConnected){
			foreach(NodePort n in entryPort.GetConnections()){
			SkillNode correct = n.node as SkillNode;
			//Debug.Log(correct.talent.id +"->" +this.talent.id);
			//Debug.Log(correct.skillName);
			dependencies.Add(correct.talent);
			talent.dependencies.Add(correct.talent);
			}
			
		}
		

		NodePort exitPort = GetOutputPort("exits");
		if(exitPort.IsConnected){
			foreach(NodePort n in exitPort.GetConnections()){
			SkillNode correct = n.node as SkillNode;
			//Debug.Log(correct.name);
			//Debug.Log(correct.skillName);
			children.Add(correct.talent);
			talent.children.Add(correct.talent);
			}
			//talent.children = children;
		}
		
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


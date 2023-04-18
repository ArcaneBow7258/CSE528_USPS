using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.IO;
public enum SKILLTYPE
    {
        PASSIVE=0,
        ABILITY=1,
        PROC=2,
    }
[Serializable]
public struct passiveStat{
	public STATTYPE tag;
	public float[] value;
}
public class talent{
        public int id;
        public Vector2 pos;
        public Sprite icon;
        public string name;
        public string desc;
        private SkillTree tree;
        public SKILLTYPE type;
        public List<talent> dependencies = new List<talent>();
        public List<talent> children = new List<talent>();
        private Transform node;
        private Button button;
        private Image buttonImage;
        private UILineRenderer lr;
        public List<passiveStat> stat;
        public ActiveAbility ability;
        public talent(int i, SkillTree oak, Vector2 position, Sprite im, string n,string d, SKILLTYPE t){
            id = i; tree = oak; pos = position; icon = im; name= n; desc = d; type = t;//dependencies = dep; this.children = children;
            // why does it have to be negatiev?
            pos.y = -pos.y;
           
            //g.GetComponent<SkillNode>
        }
        
        public void addTalent(){
           
            if(tree.pp > 0){
                bool can = false;
            
                if(id != 0 ){
                    foreach(talent t in tree.playerTalents){
                        
                        can |= dependencies.Contains(t) || children.Contains(t);
                    }
                }else{
                    can = true;
                }
                if(can){              
                    tree.playerTalents.Add(this);
                    tree.pp -= 1;
                    buttonImage.color = new Color32(255,255,255,255);
                    if(children.Count > 0){
                        for(int count =0; count < children.Count; count++){
                            
                            lr.ChangeColor(count*2, new Color32(255,255,255,255));
                            //lr.ChangeColor(count*2 + 1, new Color32(255,255,255,255));
                        }
                    }
                    if(dependencies.Count >0){
                        foreach(talent t in dependencies){
                            t.lr.ChangeColor(t.children.FindIndex(u=> u.id == this.id)*2+1, new Color32(255,255,255,255));
                        }
                    }   
                }
            
            }
        }
        public void removeTalent(){
            bool can = true;
            foreach(talent t in tree.playerTalents){
                if(t.dependencies != null){
                    can &= !t.dependencies.Contains(this);
                }
            }
            can = (canRemove() || (dependencies.Count == 0 || children.Count == 0)) ^ id == 0;
            List<talent> allPaths = new List<talent>();
            allPaths.AddRange(this.children.FindAll(t => tree.playerTalents.Contains(t)) );
            allPaths.AddRange(this.dependencies.FindAll(t => tree.playerTalents.Contains(t)));
            can |= allPaths.Count == 1;
            if(can){
                tree.playerTalents.Remove(this);
                tree.equippedAbilities.Remove(this);
                tree.playerAbilities.Remove(this);
                tree.pp += 1;
                buttonImage.color = new Color32(100,100,100,255);
                if(children.Count > 0){
                    for(int count =0; count < children.Count; count++){
                        lr.ChangeColor(count*2, new Color32(100,100,100,255));
                        //lr.ChangeColor(count*2 + 1, new Color32(100,100,100,255));
                    }
                }
                if(dependencies.Count >0){
                    foreach(talent t in dependencies){
                        t.lr.ChangeColor(t.children.FindIndex(u=> u.id == this.id)*2+1, new Color32(100,100,100,255));
                    }
                }
                
            }
        }

        private bool canRemove(){
            List<talent> testList = new List<talent>();
            foreach(talent t in tree.playerTalents){
                testList.Add(t);
            }
            testList.Remove(this);
            List<talent> foundList = new List<talent>();
            foundList.Add(this);
            searchTree(this.tree.playerTalents.Find(t => t.id == 0),testList, ref foundList);
            foundList.Remove(this);
            return testList.Count == foundList.Count;
        }
        private void searchTree(talent origin, List<talent>  test, ref List<talent> foundList){
            foundList.Add(origin);
            List<talent> allPaths = new List<talent>();
            allPaths.AddRange(origin.children.FindAll(t => tree.playerTalents.Contains(t)) );
            allPaths.AddRange(origin.dependencies.FindAll(t => tree.playerTalents.Contains(t)));
            //Debug.Log(allPaths.Count);
            allPaths.Except(foundList);
            //Debug.Log(allPaths.Count);
            //Debug.Log("origin" + origin.name);
            foreach(talent t in allPaths){
                //Debug.Log(t.name);
                if(!foundList.Contains(t)) {
                    searchTree(t, test, ref foundList);
                }
            }
        }
        public void draw(){
            node = SkillTreeDisplay.Instantiate(tree.prefab, parent:tree.display.transform).transform;
            //node.transform.
            node.gameObject.name = id +name;
            //Debug.Log(pos);
            
            node.localPosition = (new Vector3(pos.x, pos.y, 0));
            (buttonImage = node.GetComponent<Image>()).sprite = icon;
            button = node.GetComponent<Button>();
            button.onClick.AddListener(delegate{
                if(tree.playerTalents.Contains(this)) {this.removeTalent();}
                else {this.addTalent();}
            });
            ToolTip tooltip = node.GetComponent<ToolTip>();
            tooltip.UpdateText("<b>" + name + "</b>\n" + desc);
            
            //resizing
            switch(type){
                case SKILLTYPE.PASSIVE:
                    Vector3 scale = 
                    button.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                    tooltip.tooltip.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                case SKILLTYPE.ABILITY:
                    button.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                    tooltip.tooltip.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                case SKILLTYPE.PROC:
                    button.transform.localScale = new Vector3(1,1,1);
                    tooltip.tooltip.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                default:
                    button.transform.localScale = new Vector3(1,1,1);
                    tooltip.tooltip.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                
                    
            }
            //drwaing lines 
            if(children.Count > 0){
                lr = new GameObject(id+"childrenLines").AddComponent<UILineRenderer>();
                lr.gameObject.AddComponent<CanvasRenderer>();
                for(int count = 0; count < children.Count; count++){
                    lr.AddPoint(pos, new Color32(100,100,100,255));
                    lr.AddPoint(children[count].pos, new Color32(100,100,100,255));
                }
                lr.transform.SetParent(tree.display.transform, false);
                lr.transform.localPosition = new Vector3(0,0,0);
                lr.transform.SetAsFirstSibling();
                
            }
            //tooltip set up
            
        }
    }
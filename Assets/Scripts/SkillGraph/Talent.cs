using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public enum SKILLTYPE
    {
        PASSIVE=0,
        ABILITY=1,
        PROC=2,
    }
public class talent{
        public int id;
        public Vector2 pos;
        public Sprite icon;
        public string name;
        string desc;
        private SkillTree tree;
        SKILLTYPE type;
        public List<talent> dependencies = new List<talent>();
        public List<talent> children = new List<talent>();
        private Transform node;
        private Button button;
        private Image buttonImage;
        private UILineRenderer lr;
        public talent(int i, SkillTree oak, Vector2 position, Sprite im, string n,string d, SKILLTYPE t){
            id = i; tree = oak; pos = position; icon = im; name= n; desc = d; type = t;//dependencies = dep; this.children = children;
           
            //g.GetComponent<SkillNode>
        }
        public void draw(){
            node = SkillTree.Instantiate(tree.prefab, parent:tree.display.transform).transform;
            node.gameObject.name = id +name;
            Debug.Log(pos);
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
                    tooltip.tooltip.transform.localScale = new Vector3(4f, 4f, 4f);
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
        public void addTalent(){
           
            if(tree.pp > 0){
                bool can = true;
            
                if(dependencies != null){
                    foreach(talent t in dependencies){
                        
                        can &= tree.playerTalents.Contains(t);
                    }
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
            if(can){
                tree.playerTalents.Remove(this);
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
    }
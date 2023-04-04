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
        
        SKILLTYPE type;
        public List<talent> dependencies = new List<talent>();
        private Transform node;
        private Button button;
        private Image buttonImage;
        public talent(int i, Vector2 position, Sprite im, string n,string d, SKILLTYPE t, List<talent> dep){
            id = i; pos = position; icon = im; name= n; desc = d; type = t;dependencies = dep;
           
            //g.GetComponent<SkillNode>
        }
        public void draw(){
            node = SkillTree.Instantiate(SkillTree.Instance.prefab, parent:SkillTree.Instance.display.transform).transform;
            node.Translate(new Vector3(pos.x, pos.y, 0));
            (buttonImage = node.GetComponent<Image>()).sprite = icon;
            button = node.GetComponent<Button>();
            button.onClick.AddListener(delegate{
                if(SkillTree.Instance.playerTalents.Contains(this)) {this.removeTalent();}
                else {this.addTalent();}
            });
            if(dependencies != null){
                
                
                for(int count = 0; count < dependencies.Count; count++){
                   // UILineRenderer.Instance.AddPoint(new Vector2(pos.x, pos.y));
                }
                
            }
        }
        public void addTalent(){
           
            if(SkillTree.Instance.pp > 0){
                bool can = true;
            
                if(dependencies != null){
                    foreach(talent t in dependencies){
                        Debug.Log("test");
                        can &= SkillTree.Instance.playerTalents.Contains(t);
                    }
                }
                if(can){
                    SkillTree.Instance.playerTalents.Add(this);
                    SkillTree.Instance.pp -= 1;
                    buttonImage.color = new Color32(255,255,255,255);
                }
            
            }
        }
        public void removeTalent(){
            bool can = true;
            foreach(talent t in SkillTree.Instance.playerTalents){
                if(t.dependencies != null){
                    can &= !t.dependencies.Contains(this);
                }
            }
            if(can){
                SkillTree.Instance.playerTalents.Remove(this);
                SkillTree.Instance.pp += 1;
                buttonImage.color = new Color32(100,100,100,255);
            }
        }
    }
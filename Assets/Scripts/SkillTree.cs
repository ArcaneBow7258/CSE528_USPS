using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SkillTree : MonoBehaviour
{
    public static SkillTree Instance;
    public GameObject display;
    public GameObject prefab;
    public int pp;
    public enum SKILLTYPE
    {
        STAT=0,
        ACTIVE=1,
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
        public talent(int i, Vector2 position, string ic, string n,string d, SKILLTYPE t, List<talent> dep){
            id = i; pos = position; icon = Resources.Load<Sprite>(ic); name= n; desc = d; type = t;dependencies = dep;
            node = Instantiate(SkillTree.Instance.prefab, parent:SkillTree.Instance.display.transform).transform;
            node.Translate(new Vector3(pos.x, pos.y, 0));
            (buttonImage = node.GetComponent<Image>()).sprite = icon;
            button = node.GetComponent<Button>();
            button.onClick.AddListener(delegate{
                if(SkillTree.Instance.playerTalents.Contains(this)) {this.removeTalent();}
                else {this.addTalent();}
            });
            if(dependencies != null){
                
                
                for(int count = 0; count < dependencies.Count; count++){
                    UILineRenderer.Instance.AddPoint(pos.x, pos.y);
                }
                
            }
            //g.GetComponent<SkillNode>
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
    
    public SortedList<int,talent> allTalents = new SortedList<int,talent>();
    public List<talent> playerTalents = new List<talent>();
    public void Awake(){
        
        if(Instance != null){
            Debug.Log("TWO Skil trees bakana");
        }else{Instance = this;}
        List<talent> dep = new List<talent>();

        allTalents.Add(0,
            new talent(0,0,0,"flash", "Flash","Copyrighted", SKILLTYPE.STAT, null)
            );

        dep.Add(allTalents[0]);
        allTalents.Add(1,
            new talent(1,0,200,"flash2", "Flash2","Copyrighted", SKILLTYPE.STAT,dep)
            );
        dep.Remove(allTalents[0]);
        
    
        
    }
    public void Start(){
        
        
    }
}

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
        public int x;
        public int y;
        public Sprite icon;
        public string name;
        string desc;
        SKILLTYPE type;
        public List<talent> dependencies = new List<talent>();
        private Button button;
        private Image buttonImage;
        public talent(int i, int xPos, int yPos, string ic, string n,string d, SKILLTYPE t, List<talent> dep){
            id = i; x = xPos; y = yPos; icon = Resources.Load<Sprite>(ic); name= n; desc = d; type = t;dependencies = dep;
            Transform g = Instantiate(SkillTree.Instance.prefab, parent:SkillTree.Instance.display.transform).transform;
            g.Translate(new Vector3(x, y, 0));
            (buttonImage = g.GetComponent<Image>()).sprite = icon;
            button = g.GetComponent<Button>();
            button.onClick.AddListener(delegate{
                if(SkillTree.Instance.playerTalents.Contains(this)) {this.removeTalent();}
                else {this.addTalent();}
            });
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
        Debug.Log(allTalents[0].name);
        allTalents.Add(1,
            new talent(1,0,400,"flash2", "Flash2","Copyrighted", SKILLTYPE.STAT,dep)
            );
        dep.Remove(allTalents[0]);
        
    
        
    }
    public void Start(){
        
        
    }
}

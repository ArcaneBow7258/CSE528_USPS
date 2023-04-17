using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject
{
    public GameObject owner;
    public float cooldown;
    public float colldownMax;
    public List<string> tags = new List<string>();


    public abstract void activate();
    [CreateAssetMenu(menuName ="ActiveAbility/a_flash", fileName ="a_flash")]
    public class a_flash : ActiveAbility{

        public override void activate(){

        }
    }
    [CreateAssetMenu(menuName ="ActiveAbility/a_blast", fileName ="a_blast")]
    public class a_blast : ActiveAbility{

        public override void activate(){

        }
    }
    [CreateAssetMenu(menuName ="ActiveAbility/a_shield", fileName ="a_shield")]
    public class a_shield : ActiveAbility{

        public override void activate(){
            
        }
    }
}


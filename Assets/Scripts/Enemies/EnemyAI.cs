using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.AI.Navigation;
using Unity.AI;
using System.Linq;
public class EnemyAI : NetworkBehaviour
{
    public enum EnemyState{Sit, ChasePlayer, AttackPlayer};
    public EnemyState currentState;
    public Sight sightSensor;
    private bool pathOpen = false;
    public float playerAttackDistance; 
    [Header("Attacks")]
    public GameObject bullet;
    public float damage;
    public float fireRate;
    public float lastShootTime;
    public GameObject shootpoint;
    private UnityEngine.AI.NavMeshAgent agent;

    private UnityEngine.AI.NavMeshPath path ;
    //private Animator animator;
    private void Awake(){
        path = new UnityEngine.AI.NavMeshPath();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = IsSpawned;
        sightSensor = GetComponent<Sight>();
        
        //animator = GetComponent<Animator>();
    }
    public override void OnNetworkSpawn(){
        agent.enabled = IsServer;
        sightSensor.enabled = IsServer;
        if(!IsServer) return;
        agent.CalculatePath((from player in GameManager.Instance.players
                            orderby Vector3.Distance(transform.position, player.transform.position)
                            select player.transform.position).First()
                    , path);
        if(path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete){ // we can reach player traveling through rooms, just schase
            pathOpen = true;
            currentState = EnemyState.ChasePlayer;
        }

       
    }
    public GameObject GetClosestPlayer(){
        return (from player in GameManager.Instance.players
                            orderby Vector3.Distance(transform.position, player.transform.position)
                            select player).First();
    }
    void Update(){
        if(IsServer){
            if (currentState == EnemyState.Sit){
                //animator.SetBool("Moving", false);
                //animator.SetBool("Attacking", false);
                DoNothing();}
            else if (currentState == EnemyState.ChasePlayer){
                //animator.SetBool("Moving", true);
                //animator.SetBool("Attacking", false);
                ChasePlayer();
                }
            else {
                
                //animator.SetBool("Attacking", true);
                //animator.SetBool("Moving", false);
                AttackPlayer();
            }
        }
    }
    void DoNothing(){
        
        if(sightSensor.detectedObject != null){
            currentState = EnemyState.ChasePlayer;
            return;
        }
    }
    void ChasePlayer(){
        agent.isStopped = false;
        float distanceToPlayer= 0;
        if(pathOpen){
            GameObject player = GetClosestPlayer();
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            agent.SetDestination(GetClosestPlayer().transform.position);
        }else{
            if(sightSensor.detectedObject == null){
            currentState = EnemyState.Sit;
            return;
            }
            distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
            agent.SetDestination(sightSensor.detectedObject.transform.position);
        }
        
        
        
        
        if (distanceToPlayer < playerAttackDistance){
            currentState = EnemyState.AttackPlayer;
            lastShootTime = Time.time; //finish animation before attacking, not on contact.
        }
        
    }
    public virtual void  AttackPlayer(){
        agent.isStopped = true;
        if(sightSensor.detectedObject == null){
            currentState = EnemyState.Sit;
            return;
        }
        //Debug.Log(sightSensor.detectedObject.transform.position);
        LookTo(sightSensor.detectedObject.transform.position);
        //Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f){
            currentState = EnemyState.ChasePlayer;
        }else{
            if(Time.time > lastShootTime + fireRate){
                lastShootTime = Time.time;
                if(bullet != null && shootpoint != null){
                    GameObject b = Instantiate(bullet, shootpoint.transform);
                    b.transform.LookAt(sightSensor.detectedObject.transform.position + Vector3.up*1);
                    //b.GetComponent<ContactDamage>().damage = damage;
                }else{
                    //okay will probalby ufkc up later but thats okay
                    
                    //sightSensor.detectedObject.transform.parent.GetComponent<PlayerStats>().TakeDamageClientRpc(damage);
                }
            }
        }


    }
    void Shoot(){
        var timeSinceLastShoot  = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate){
            lastShootTime = Time.time;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
    void LookTo(Vector3 targetPosition){
        //Debug.Log(transform.position);
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.position);
        directionToPosition.y = 0;
        transform.forward = directionToPosition;

    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
   
    }
}

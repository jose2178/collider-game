using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text lifeText;
    [SerializeField]
    Text finalScore;

    private int score;
    public int lifes;

    Vector3 targetPosition;
    public NavMeshAgent agent;
    public GameObject gameOver;
    const int LEFT_MOUSE_BUTTON = 0;
    

    //creando instancias de enemigos, perro, GameController.
    private PatrolDog dog;
    private PatrolEnemy enemy;
    //private GameController gameController;

    
    private AudioSource audioSource;
    private bool condicionSonido;
    public AudioClip perdiste;
    public AudioClip risa;
    public AudioSource recoger;
    
    

    void Start()
    {
        score = 0;
        scoreText.text = "Puntos: " + score;
        lifeText.text = "Vidas: " + lifes;

        agent = GetComponent<NavMeshAgent>();
        dog = GameObject.FindWithTag("Dog").GetComponent<PatrolDog>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<PatrolEnemy>();
        //gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameOver = GameObject.FindWithTag("Gameover");
        audioSource = GetComponent<AudioSource>();

        gameOver.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        condicionSonido = true;
        
        agent = GetComponent<NavMeshAgent>();
        targetPosition = this.transform.position;
    }

    void Update()
    {
        lifeText.text = "Vidas: " + enemy.counterLifes;

        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON))
        {
            SetTargetPosition();
        }
        else if (enemy.counterLifes == 0 && condicionSonido)
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.clip = perdiste;
            audioSource.Play();            
            //Cambia a escena de reiniciar o pued invocar un canvas con un mensaje.
            StopAgents();
            StartCoroutine(RetrasoGameOver());
            finalScore.text = "Score: " + score;

            condicionSonido = false;
            
        }
        else
        {
            MovePlayer();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        recoger.Play();
        Destroy(other.gameObject);
        score++;
        scoreText.text = "Puntos: " + score;
    }

    private void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            targetPosition = ray.GetPoint(point);
        }

    }

    private void MovePlayer()
    {
        agent.SetDestination(targetPosition);

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }

    private void StopAgents()
    {
        dog.agent.Stop();
        enemy.agent.Stop();
    }

    IEnumerator RetrasoGameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOver.SetActive(true);
        audioSource.clip = risa;
        audioSource.Play();
    }
}

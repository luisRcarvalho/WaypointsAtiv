using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public GameObject[] waypoints; // pontos que o gameobject ira passar
    int currentWP = 0; //ponto atual
    float speed= 1.0f; //velocidade de locomação do gameobject
    float accuracy= 1.0f;// variavel para calcular a distancia para o ponto, para que possa fazer a rotação
    float rotSpeed= 0.4f; //velocidade da rotação
    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //caso os gameobjects nao forem jogados no inspector, ele coloca automaticamente de forma aleatoria os objetos com a tag
    }

    private void LateUpdate()
    {
        if (waypoints.Length == 0) return; //quando passa em todos os pontos, ele recomeça
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);//olha para a posição do ponto
        Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);// calcula a rotação que tem que realizar para ir ao ponto, utilizando o lookatgoal como referencia para saber aonde vai

        if (direction.magnitude < accuracy)// se a direção for menor que a distancia estebelecida, ele ja passa para o calculo do proximo ponto
        {
            currentWP++;//depois que passa de um ponto, passa para o proximo
            if (currentWP >= waypoints.Length)//quando o ponto atual ja é maior que o tamanho do array:
            { 
                currentWP = 0; //zera o ponto quando termina de passar em todos
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);//realiza a movimentação, utilizando o speed e normalizando o tempo com timedeltatime
    }
}
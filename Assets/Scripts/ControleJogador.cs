using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    public int amplitudeForca = 10;
    public int amplitudePulo = 10;
    Rigidbody rb;
    int pontuacao;
    int pontuacaoAlta;
    bool estaNoChao;

    TextMeshProUGUI objeto_pontuacao;
    TextMeshProUGUI objeto_pontuacao_alta;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objeto_pontuacao = GameObject.FindWithTag("Pontuacao").GetComponent<TextMeshProUGUI>();
        objeto_pontuacao_alta = GameObject.FindWithTag("PontuacaoAlta").GetComponent<TextMeshProUGUI>();
        
        pontuacao = 0;
        pontuacaoAlta = PlayerPrefs.GetInt("pontuacaoAlta", 0);
        objeto_pontuacao_alta.text = "Pontuacao Mais Alta: " + pontuacaoAlta.ToString();
        estaNoChao = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moverLateral = Input.GetAxisRaw("Horizontal");
        float moverFrente = Input.GetAxisRaw("Vertical");
        Vector3 direcao = new Vector3(moverLateral, 0.0f, moverFrente);
        rb.AddForce(direcao * amplitudeForca);
        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao) {
            rb.AddForce(Vector3.up * amplitudePulo, ForceMode.Impulse);
            estaNoChao = false;
        }
    }

    void OnTriggerEnter(Collider outro) {
        if(outro.gameObject.CompareTag("Moeda")) {
            pontuacao++;
            objeto_pontuacao.text = "Pontuacao: " + pontuacao.ToString();
            if (pontuacao > pontuacaoAlta) {
                objeto_pontuacao_alta.text = "Pontuacao Mais Alta: " + pontuacao.ToString();
                PlayerPrefs.SetInt("pontuacaoAlta", pontuacao);
            }
            Destroy(outro.gameObject);
        }
    }

    void OnCollisionEnter(Collision outro) {
        if(outro.gameObject.CompareTag("Chao")) {
            estaNoChao = true;
        }
        if (outro.gameObject.CompareTag("Inimigo")) {
            Scene cena = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cena.name);
        }
    }
}

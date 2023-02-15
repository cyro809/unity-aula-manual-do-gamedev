using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoInimigo : MonoBehaviour
{
    Rigidbody rb;
    public GameObject jogador;

    int amplitude = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicaoJogador = jogador.transform.position;
        Vector3 minhaPosicao = transform.position;
        Vector3 direcao = (posicaoJogador - minhaPosicao).normalized;
        rb.AddForce(direcao * amplitude);
    }
}

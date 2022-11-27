using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    private float[] w;
    private float b;
    private float learningRate = 0.01f;

    public Perceptron(int size)
    {
        w = new float[size];

        for (int i = 0; i < w.Length; i++)
            w[i] = Random.value;

        b = Random.value;
    }

    public int Forward(float[] input)
    {
        float output = 0;

        for (int i = 0; i < input.Length; i++)
            output += input[i] * w[i];

        if ((output + b) >= 0)
            return 1;

        return 0;
    }

    public void Train(float[] input, int expected)
    {
        int output = Forward(input);
        int erro = expected - output;

        for (int i = 0; i < input.Length; i++)
            w[i] += input[i] * erro * learningRate;

        b += erro * learningRate;
    }
}

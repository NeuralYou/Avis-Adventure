using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neuron 
{
	Neuron[] inputNueorns;
	float[] inputValues;

	Neuron[] nextLayerNeurons;
	float[] outputWeights;
	public float[] OutputWeights
	{
		get
		{
			float[] weights = new float[outputWeights.Length];
			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] = outputWeights[i];
			}

			return weights;
		}

		set { outputWeights = value; }
	}

	public float CurrentValue { get; set; }

	//For construction of an output neuron.
	public Neuron()
	{

	}

	//For construction of an input\hidden neuron.
	public Neuron(Neuron[] i_NextLayerNeurons)
	{
		this.nextLayerNeurons = (Neuron[]) i_NextLayerNeurons.Clone();
		outputWeights = new float[nextLayerNeurons.Length];

		for(int i = 0; i < outputWeights.Length; i++)
		{
			outputWeights[i] = Random.Range(-1f, 1f);
		}

	}

	public void PassInputTo(Neuron i_Neuron)
	{
		List<Neuron> list = new List<Neuron>(nextLayerNeurons);
		int index =list.IndexOf(i_Neuron);
		i_Neuron.RecieveInput(CurrentValue * outputWeights[index]);
	
	}

	public void RecieveInput(float val)
	{
		CurrentValue += val;
	}

	public void ActivateSelf()
	{
		CurrentValue = (float) System.Math.Tanh(CurrentValue);
	}

	//Can't feed forward before this neuron was fed all of its inputs.
	public void FeedForward()
	{
		for(int i = 0; i < nextLayerNeurons.Length; i++)
		{
			nextLayerNeurons[i].RecieveInput(outputWeights[i] * CurrentValue);
		}
	}

	public void Reset()
	{
		CurrentValue = 0;
	}

	public Color Mutate(float i_MutationRate)
	{
		Color color = new Color();
		for(int i = 0; i < outputWeights.Length; i++)
		{
			if(Random.Range(0, 1f) <= i_MutationRate)
			{
				outputWeights[i] += Random.Range(-3f, 3f);
				color.r += Random.Range(-0.05f, 0.05f);
				color.g += Random.Range(-0.05f, 0.05f);
				color.b += Random.Range(-0.05f, 0.05f);
			}

		}

		return color;
	}

	public float[] ExtractWeights()
	{
		float[] weights = new float[outputWeights.Length];
		for(int i = 0; i < weights.Length; i++)
		{
			weights[i] = outputWeights[i];
		}

		return weights;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInfo
{
	public Neuron[] inputs;
	public Neuron[] hidden;
	public Neuron[] outputs;

	public NetworkInfo(Neuron[] inputs, Neuron[] hidden, Neuron[] outputs)
	{
		this.inputs = (Neuron[])inputs.Clone();
		this.hidden = (Neuron[])hidden.Clone();
		this.outputs = (Neuron[])outputs.Clone();
	}
}

public class ClassicNeuralNetwork : MonoBehaviour
{
	Neuron[] inputs;
	Neuron[] hidden;
	Neuron[] outputs;
	float networkThershold;

	public void InitSelf(int i_NumberOfInputs, int i_NumberOfHidden, int i_NumberOfOutputs)
	{
		inputs = new Neuron[i_NumberOfInputs];
		hidden = new Neuron[i_NumberOfHidden];
		outputs = new Neuron[i_NumberOfOutputs];
		InitNeurons();
	}

	public void InitFromNetworkInfo(NetworkInfo i_Info)
	{
		this.inputs = new Neuron[i_Info.inputs.Length];
		this.hidden = new Neuron[i_Info.hidden.Length];
		this.outputs = new Neuron[i_Info.outputs.Length];

		for (int i = 0; i < outputs.Length; i++)
		{
			outputs[i] = new Neuron();
		}

		for (int i = 0; i < hidden.Length; i++)
		{
			hidden[i] = new Neuron(outputs);
			hidden[i].OutputWeights = i_Info.hidden[i].OutputWeights;
		}

		for (int i = 0; i < inputs.Length; i++)
		{
			inputs[i] = new Neuron(hidden);
			inputs[i].OutputWeights = i_Info.inputs[i].OutputWeights;
		}
	}

	private void InitNeurons()
	{
		for (int i = 0; i < outputs.Length; i++)
		{
			outputs[i] = new Neuron();
		}

		for (int i = 0; i < hidden.Length; i++)
		{
			hidden[i] = new Neuron(outputs);
		}

		for (int i = 0; i < inputs.Length; i++)
		{
			inputs[i] = new Neuron(hidden);
		}
	}

	public float[] FeedForward(float[] i_Inputs)
	{
		FeedInput(i_Inputs);
		FeedInputToHidden();
		FeedHiddenToOutput();

		return GenerateOutput();
	}

	public void FeedInput(float[] i_Inputs)
	{
		for(int i = 0; i < inputs.Length; i++)
		{
			inputs[i].RecieveInput(i_Inputs[i]);
		}
	}


	public void FeedInputToHidden()
	{
		foreach(Neuron hid in hidden)
		{
			foreach(Neuron input in inputs)
			{
				input.PassInputTo(hid);
			}

			hid.ActivateSelf();
		}
	}

	public void FeedHiddenToOutput()
	{
		networkThershold = 0f;
		foreach (Neuron output in outputs)
		{
			foreach (Neuron hid in hidden)
			{
				if(hid.CurrentValue > networkThershold)
				{
					hid.PassInputTo(output);
				}
			}

			output.ActivateSelf();
		}
	}

	public float[] GenerateOutput()
	{
		float[] networkOutputs = new float[outputs.Length];
		for(int i = 0; i < networkOutputs.Length; i++)
		{
			networkOutputs[i] = outputs[i].CurrentValue;
		}

		return networkOutputs;
	}

	public void ResetNetwork()
	{
		foreach(Neuron n in inputs)
		{
			n.Reset();
		}

		foreach (Neuron n in hidden)
		{
			n.Reset();
		}

		foreach (Neuron n in outputs)
		{
			n.Reset();
		}
	}
	
	public void MutateNetwork(float i_MutationRate)
	{
		Color color = new Color();
		foreach(Neuron n in inputs)
		{
			color += n.Mutate(i_MutationRate);
		}

		foreach(Neuron n in hidden)
		{
			color += n.Mutate(i_MutationRate);
		}

		GetComponent<SpriteRenderer>().color += color;
	}

	public NetworkInfo GetNetworkInfo()
	{
		return new NetworkInfo(inputs, hidden, outputs);
	}

}






















	//int[] layers;
	//float[][][] weights;
	//float[][] neurons;

	//public void InitSelf(int[] layers)
	//{
	//	this.layers = (int[])layers.Clone();
	//	initLayers();
	//}

	//public void InitSelf(int i_InputSize, int i_HiddenSize, int i_OutputSize)
	//{
	//	initWeights(i_InputSize, i_HiddenSize, i_OutputSize);
	//}

	//private void initLayers()
	//{
	//	List<float[]> list = new List<float[]>();
	//	for(int i = 0; i < layers.Length; i++)
	//	{
	//		list.Add(new float[layers[i]]);
	//	}

	//	neurons = list.ToArray();
	//}

	//private void initNeurons()
	//{

	//}

	//private void initWeights(int i_InputSize, int i_HiddenSize, int i_OutputSize)
	//{
	//	//weights = new float[3][][];
	//	//weights[0] = new float[i_InputSize][];
	//	//weights[1] = new float[i_HiddenSize][];
	//	//weights[2] = new float[i_OutputSize][];

	//	//for (int i = 0; i < weights.Length; i++)
	//	//{
	//	//	for(int j = 0; j < weights[i].Length; j++)
	//	//	{
	//	//		weights[i][j] = new float[]
	//	//	}
	//	//}
	//}


	//private void calcActivation()
	//{

	//}



	//private float activate(float val)
	//{
	//	return (float) System.Math.Tanh(val);
	//}

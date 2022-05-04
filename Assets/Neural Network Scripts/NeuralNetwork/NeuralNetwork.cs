using Newtonsoft.Json;
using System;

[System.Serializable]
public class NeuralNetwork : IComparable<NeuralNetwork>
{
	[JsonProperty] public InputNeuron[] inputs;
	[JsonProperty] public HiddenNeuron[] hidden;
	[JsonProperty] public OutputNeuron[] outputs;
	float networkThreshold;
	public float Fitness { get; set; }

	public void SetCallbacks(params Action[] actions)
	{
		for (int i = 0; i < outputs.Length; i++)
		{
			outputs[i].m_Callback = actions[i];
		}
	}

	public void FeedForward(params float[] i_Inputs)
	{
		FeedInput(i_Inputs);
		FeedInputToHiddens();
		FeedHiddenToOutput();
	}

	public void FeedInput(params float[] i_Inputs)
	{
		for (int i = 0; i < inputs.Length; i++)
		{
			inputs[i].RecieveInput(i_Inputs[i]);
		}
	}


	public void FeedInputToHiddens()
	{
		foreach (InputNeuron input in inputs)
		{
			input.FeedForward(hidden);
			input.Reset();
		}

		foreach (HiddenNeuron hid in hidden)
		{
			hid.Activate(networkThreshold);
		}
	}

	public void FeedHiddenToOutput()
	{
		foreach (HiddenNeuron hid in hidden)
		{
			hid.FeedForward(outputs);
			hid.Reset();
		}

		foreach (OutputNeuron output in outputs)
		{
			output.Activate(networkThreshold);
			output.Reset();
		}
	}

	public int CompareTo(NeuralNetwork other)
	{
		return (int) (this.Fitness - other.Fitness);
	}
}
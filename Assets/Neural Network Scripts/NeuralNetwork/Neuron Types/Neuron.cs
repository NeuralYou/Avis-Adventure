using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public abstract class Neuron
{
	protected float currentValue;
	[JsonProperty] protected float[] outputWeights;
	protected bool active;

	public virtual void RecieveInput(float i_InputVal)
	{
		currentValue += i_InputVal;
	}

	public virtual void Activate(float i_Threshold)
	{
		currentValue = (float)Math.Tanh(currentValue);
		
		if(currentValue > i_Threshold) { active = true; }
		else { active = false; }
	}

	public void Reset()
	{
		currentValue = 0;
	}

	public void FeedForward(Neuron[] i_NextLayerNeurons)
	{
		for(int i = 0; i < i_NextLayerNeurons.Length; i++)
		{
			i_NextLayerNeurons[i].RecieveInput(currentValue * outputWeights[i]);
		}
	}
}

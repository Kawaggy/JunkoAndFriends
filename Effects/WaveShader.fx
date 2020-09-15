sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 WaveShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    //float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float frameY = coords.y;
	float newCoords = sin(uTime * 3);
	//float newCoords = sin(uTime) * 0.10;
	newCoords *= (1 - frameY);
	
	float newSin = sin(uTime / 5) * 0.30;
	
	if (frameY < 0.10 + newSin)
	{
		coords.x = coords.x - (newCoords * 0.05);
	}
	else if (frameY > 0.10 + newSin && frameY < 0.20 + newSin)
	{
		coords.x = coords.x - (newCoords * 0.10);
	}
	else if (frameY > 0.20 + newSin && frameY < 0.30 + newSin)
	{
        coords.x = coords.x - (newCoords * 0.1575);
    }
	else if (frameY > 0.30 + newSin && frameY < 0.40 + newSin)
	{
        coords.x = coords.x - (newCoords * 0.10);
    }
	else if (frameY > 0.40 + newSin && frameY < 0.50 + newSin)
	{
        coords.x = coords.x - (newCoords * 0.05);
    }
	else if (frameY > 0.50 + newSin && frameY < 0.60 + newSin)
	{
        coords.x = coords.x + (newCoords * 0.05);
    }
	else if (frameY > 0.60 + newSin && frameY < 0.70 + newSin)
	{
        coords.x = coords.x + (newCoords * 0.10);
    }
	else if (frameY > 0.70 + newSin && frameY < 0.80 + newSin)
	{
		coords.x = coords.x + (newCoords * 0.1575);
	}
	else if (frameY > 0.80 + newSin && frameY < 0.90 + newSin)
	{
        coords.x = coords.x + (newCoords * 0.10);
    }
	else if (frameY > 0.90 + newSin)
	{
        coords.x = coords.x + (newCoords * 0.05);
    }
	
	//if (frameY < 0.50)
	//{
	//	coords.x = coords.x - (newCoords * 0.10);
	//}
	//else if (frameY >= 0.50)
	//{
	//	coords.x = coords.x - (newCoords * 0.20);
	//}
	return tex2D(uImage0, coords);
}

technique Technique1
{
    pass WaveShaderPass
    {
		PixelShader = compile ps_2_0 WaveShader();
	}
}
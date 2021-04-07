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
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    coords.x += sin((100 * (coords.y * 0.1)) + uTime * 8) * 0.06 * (1 - coords.y);
    return tex2D(uImage0, coords);
}

technique Technique1
{
    pass WaveShaderPass
    {
		PixelShader = compile ps_2_0 WaveShader();
	}
}
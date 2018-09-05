// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/RotationShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TransTex("Transparency Texture", 2D) = "white" {}
		_Color("Tint Color", Color) = (0, 1, 0, 1)
		_Angle("Angle", Range(-5.0,  5.0)) = 0.0
		_MoveDampen("Randomness Dampener", Float) = .2
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha One
		Cull Off

		Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#pragma multi_compile_fog
		
		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float2 uv2 : TEXCOORD1;
		
		};
		struct v2f 
		{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
		UNITY_FOG_COORDS(1)
		};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	sampler2D _TransTex;
	float4 _TransTex_ST;
	float4 _Color;
	float _MoveDampen;
	float _Angle;

	v2f vert(appdata v)
	{
		v2f o;
		_MainTex_ST.x = .5f + sin(_Time.x) * .2f;
		_MainTex_ST.y = .5f + cos(_Time.x) * .2f;
		_TransTex_ST.x = .5f + sin(_Time.x) * .2f;
		_TransTex_ST.y = .5f + cos(_Time.x) * .2f;
		_MainTex_ST.z = sin(_Time.x + 7) * -_MoveDampen;
		_MainTex_ST.w = cos(_Time.x + 5) * _MoveDampen;
		_TransTex_ST.z = sin(_Time.x + 2) * _MoveDampen;
		_TransTex_ST.w = cos(_Time.x) * -_MoveDampen;
		o.vertex = UnityObjectToClipPos(v.vertex);
		v.uv = TRANSFORM_TEX(v.uv, _MainTex);
		v.uv2 = TRANSFORM_TEX(v.uv2, _TransTex);
		UNITY_TRANSFER_FOG(o, o.vertex);

		//change angle
		_Angle += 8 *sin(_Time.x);

		//o.vertex = UnityObjectToClipPos(v.vertex);

		// Pivot
		float2 pivot = TRANSFORM_TEX(float2(.5f + _MainTex_ST.z, .5f + _MainTex_ST.w), _MainTex);
		//float2 pivot = float2(0.5, 0.5);
		//float2 pivot = float2(0.25f, 0.25f);
		// Rotation Matrix
		float cosAngle = cos(_Angle);
		float sinAngle = sin(_Angle);
		float2x2 rot = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);

		// Rotation consedering pivot
		float2 uv = v.uv.xy - pivot;
		o.uv = mul(rot, uv);
		o.uv += TRANSFORM_TEX(pivot, _MainTex);

		return o;
	}


	fixed4 frag(v2f i) : SV_Target
	{
		// Texel sampling
		fixed4 col = tex2D(_MainTex, i.uv);
		fixed4 trans = tex2D(_TransTex, i.uv2);
		col.r = lerp(col.r, _Color.r, .2f);
		col.g = lerp(col.g, _Color.g, .2f);
		col.b = lerp(col.b, _Color.b, .8f);
		col.a *= trans.r;
		return col;
	}

		ENDCG
	}
	}
}

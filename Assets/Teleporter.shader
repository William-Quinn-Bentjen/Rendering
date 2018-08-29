// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Teleporter"
{
	Properties
	{
		//0,0,0 = black
		//1,1,1 = white
		_MainTex ("Texture", 2D) = "white" {}
		_TransTex ("Transparency Texture", 2D) = "white" {}
		_Color("Black Color", Color) = (0, 1, 0, 1)
		_2ndColor("White Color", Color) = (1, 1, 0, 1)
		_MoveDampen("Randomness Dampener", Float) = .2
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha One
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				//float4 objVertex : TEXCOORD1;
			};
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _TransTex;
			float4 _TransTex_ST;
			float4 _2ndColor;

			v2f vert (appdata v)
			{
				v2f o;
				//o.objVertex = mul(unity_ObjectToWorld, v.vertex);
				_TransTex_ST.x = sin(_Time.x);
				_TransTex_ST.y = cos(_Time.x);
				_TransTex_ST.z = tan(_Time.x);
				_TransTex_ST.w = cos(_Time.x);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv2, _TransTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 trans = tex2D(_TransTex, i.uv2);
				col.a = trans.r;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

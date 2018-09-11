Shader "Custom/Teleporter2" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		//_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Tess("Tessellation", Range(1,8)) = 4
		_NoiseScale("Noise Scale", float) = 1
		_NoiseColorScale("Noise Color Scale", float) = 1
		_NoiseFrequency("Noise Frequency", float) = 1
		_NoiseOffset("Noise Offset", Vector) = (0,0,0,0)
		_MaxAlpha("Maximum Alpha", Range(0,1)) = .75
		_MinAlpha("Minimum Alpha", Range(0,1)) = 1
		_TimeScale("Time Scaler", float) = 10
		
	}
	SubShader {
		//Tags { "RenderType"="Opaque" }
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off
		Blend SrcAlpha One
		Cull Off

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows tessellate:tess vertex:vert alpha:fade

		#pragma target 4.6

		#include "noiseSimplex.cginc"
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 tangent : TANGENT;
			float2 texcoord : TEXCOORD0;
		};

		//sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		float _Tess;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _NoiseScale, _NoiseFrequency, _NoiseColorScale;
		half _Alpha;
		float4 _NoiseOffset;
		float _MaxAlpha, _MinAlpha, _TimeScale;


		float4 tess()
		{
			return _Tess;
		}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		//UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		//UNITY_INSTANCING_BUFFER_END(Props)
		void vert(inout appdata v)
		{
			//time change
			_NoiseOffset.y += _Time.x * 10;
			//vertex manupulation and normal recalculation
			float3 v0 = v.vertex.xyz;
			float3 bitangent = cross(v.normal, v.tangent.xyz);
			float3 v1 = v0 + (v.tangent.xyz * 0.01);
			float3 v2 = v0 + (bitangent * 0.01);
			float ns0 = _NoiseScale * snoise(float3(v0.x + _NoiseOffset.x, v0.y + _NoiseOffset.y, v0.z + _NoiseOffset.z) * _NoiseFrequency);
			v0.xyz += ((ns0 + 1) / 2) * v.normal;
			float ns1 = _NoiseScale * snoise(float3(v1.x + _NoiseOffset.x, v1.y + _NoiseOffset.y, v1.z + _NoiseOffset.z) * _NoiseFrequency);
			v1.xyz += (ns1 + 1) / 2 * v.normal;
			float ns2 = _NoiseScale * snoise(float3(v2.x + _NoiseOffset.x, v2.y + _NoiseOffset.y, v2.z + _NoiseOffset.z) * _NoiseFrequency);
			v2.xyz += (ns2 + 1) / 2 * v.normal;
			float3 vn = cross(v2 - v0, v1 - v0);
			v.normal = normalize(-vn);
			v.vertex.xyz = v0;
			//just vertex manipulation
			//float noise = _NoiseScale * snoise(float3(v.vertex.x + _NoiseOffset.x, v.vertex.y + _NoiseOffset.y, v.vertex.z + _NoiseOffset.z) * _NoiseFrequency);
			//v.vertex.y += noise;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			//float noise = _NoiseColorScale * snoise(float3(IN.worldPos.x + _NoiseOffset.x, IN.worldPos.y + _Time.x + _NoiseOffset.y, IN.worldPos.z + _Time.x + _NoiseOffset.z) * _NoiseFrequency);
			float noise = _NoiseColorScale * snoise(float3(IN.worldPos.x + _Time.w + _NoiseOffset.x, IN.worldPos.x + _Time.y + _NoiseOffset.y, IN.worldPos.x + _Time.z + _NoiseOffset.z) * _NoiseFrequency);
			o.Albedo = float3(_Color.r * noise, _Color.g/* * noise*/, _Color.b * noise);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = clamp((_Alpha+ noise/2),_MinAlpha, _MaxAlpha);
			//o.Alpha = _NoiseScale * snoise(float3(IN.worldPos.x + _NoiseOffset.x, IN.worldPos.y + _NoiseOffset.y, IN.worldPos.z + _NoiseOffset.z) * _NoiseFrequency);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

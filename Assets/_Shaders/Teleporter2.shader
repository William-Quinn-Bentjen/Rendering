Shader "Custom/Teleporter2" {
	Properties{
		_EmissionColor("EmissionColor", Color) = (1,1,1,1)
		_Color("Color", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		_MainTex ("Grayscale texture", 2D) = "white" {}
		_MainTex1("other grayscale texture", 2D) = "white" {}

		_BumpTex("Bump map 1", 2D) = "white" {}
		_BumpTex1("Bump map 2", 2D) = "white" {}

		_Glossiness("Smoothness", Range(0.3,1)) = 1
		//_Metallic("Metallic", Range(0,1)) = 0.0
		_Tess("Tessellation", Range(1,8)) = 4
		_NoiseScale("Noise Scale", float) = .45
		_NoiseColorScale("Noise Color Scale", float) = 8.02
		_NoiseFrequency("Noise Frequency", float) = 1.36
		_NoiseOffset("Noise Offset", Vector) = (0,0,0,0)
		_ColorOffset("Color Offset", Vector) = (-1,-1,2,2)
		_BumpOffset("Bump Offset", Vector) = (1,1.5,-1,1)
		_MaxAlpha("Maximum Alpha", Range(0,1)) = 1
		_MinAlpha("Minimum Alpha", Range(0,1)) = 0.254
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
		#pragma surface surf BlinnPhong fullforwardshadows tessellate:tess vertex:vert alpha:fade

		#pragma target 4.6

		#include "noiseSimplex.cginc"
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 tangent : TANGENT;
			float2 texcoord : TEXCOORD0;
		};

		sampler2D _MainTex;
		sampler2D _MainTex1;
		sampler2D _BumpTex;
		sampler2D _BumpTex1;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		float _Tess;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _Color2;
		fixed4 _EmissionColor;
		float _NoiseScale, _NoiseFrequency, _NoiseColorScale;
		half _Alpha;
		float4 _NoiseOffset, _ColorOffset, _BumpOffset;
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

		void surf (Input IN, inout SurfaceOutput o) {
			//albedo texture and normal map movement
			fixed4 mainColor = tex2D(_MainTex, IN.uv_MainTex + float2(_Time.x * _ColorOffset.x, _Time.x * _ColorOffset.y));
			fixed4 mainColor1 = tex2D(_MainTex1, IN.uv_MainTex + float2 (_Time.x * _ColorOffset.y, _Time.x * _ColorOffset.z));
			fixed4 bumpColor = tex2D(_BumpTex, IN.uv_MainTex + float2(_Time.x * _BumpOffset.x, _Time.x * _BumpOffset.y));
			fixed4 bumpColor1 = tex2D(_BumpTex1, IN.uv_MainTex + float2(_Time.x * _BumpOffset.z, _Time.x * _BumpOffset.w));
			o.Normal = UnpackNormal(bumpColor + bumpColor1);

			/*float4 blackOfFirst = max(float4(.5,.5,.5,1),float4(1, 1, 1, 1) - mainColor);
			float4 blackOfSecond = max(float4(.5,.5,.5,1),float4(1, 1, 1, 1) - mainColor1);
			fixed4 col = mainColor + mainColor1;
			*/
			fixed3 main = lerp(fixed3(0, 0, 0), _Color, mainColor.r);
			fixed3 main1 = lerp(_Color2, fixed3(1, 1, 0), mainColor1.r);
			fixed3 col = main1 + main;
			o.Albedo = col;
			o.Emission = col.r * _EmissionColor * (sin(_Time.x * _TimeScale)/2+.75);

			//lerp(_Color1.rgb, _Color2.rgb, Minimum(1, col.r));
			//o.Albedo = (blackOfFirst * _Color) + (blackOfSecond * _Color2);
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			//float noise = _NoiseColorScale * snoise(float3(IN.worldPos.x + _NoiseOffset.x, IN.worldPos.y + _Time.x + _NoiseOffset.y, IN.worldPos.z + _Time.x + _NoiseOffset.z) * _NoiseFrequency);
			//float noise = _NoiseColorScale * snoise(float3(IN.worldPos.x + _Time.w + _NoiseOffset.x, IN.worldPos.x + _Time.y + _NoiseOffset.y, IN.worldPos.x + _Time.z + _NoiseOffset.z) * _NoiseFrequency);
			//o.Albedo = float3(_Color.r * noise, _Color.g/* * noise*/, _Color.b * noise);
			// Metallic and smoothness come from slider variables
			o.Specular = _Glossiness;
			o.Gloss = _Glossiness;// _SpecColor.a;
			//o.Emission = _EmissionColor;
			o.Alpha = _MaxAlpha;// clamp((_Alpha + noise / 2), _MinAlpha, _MaxAlpha);
			//o.Alpha = _NoiseScale * snoise(float3(IN.worldPos.x + _NoiseOffset.x, IN.worldPos.y + _NoiseOffset.y, IN.worldPos.z + _NoiseOffset.z) * _NoiseFrequency);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
